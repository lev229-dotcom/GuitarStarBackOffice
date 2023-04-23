using DataBaseService.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Mail;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Portal.TelegramBotService;

public class ConfirmOrderCommand
{
    private readonly ITelegramBotClient bot;
    private readonly CreateEmailBodyRule createEmailBodyRule;


    private readonly OrderService OrderService;

    public ConfirmOrderCommand(ITelegramBotClient bot, CreateEmailBodyRule createEmailBodyRule, OrderService orderService)
    {
        this.bot = bot;
        this.createEmailBodyRule = createEmailBodyRule;
        OrderService = orderService;
    }
    public async Task Execute(CallbackQuery callbackQuery)
    {
        //Получение текущего заказа
        var order = await OrderService.GetOrderById(new Guid(callbackQuery.Data));

        if (order != null)
        {
            // Формируем сообщение
            var messageBuilder = new StringBuilder();
            messageBuilder
                .AppendLine($"✅ <b>Заказ подтвержден!</b>")
                .AppendLine($"<b>Номер заказа</b> #{order.OrderNumber}")
                .AppendLine($"<u>Контактные данные клиента</u>")
                .AppendLine($"<b>ФИО:</b> {order.CustomerName}");

            if (order.CustomerNumber is not null && order.CustomerNumber.Length > 0)
                messageBuilder.AppendLine($"<b>Номер телефона</b> {order.CustomerNumber}");

            messageBuilder
                .AppendLine($"<b>Электронная почта</b> {order.CustomerEmail}")
                .AppendLine($"<b>Адрес</b> {order.CustomerAddress}");

            // Редактируем существующее сообщение
            await bot.EditMessageTextAsync(
               chatId: callbackQuery.Message.Chat.Id,
               messageId: callbackQuery.Message.MessageId,
               parseMode: ParseMode.Html,
               text: messageBuilder.ToString());

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                try
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("yalev1312@gmail.com", "xoxcxvvoozggfmfp");
                    var body = createEmailBodyRule.Execute(order);
                    MailMessage message = new MailMessage(
                                             "yalev1312@gmail.com",
                                             order.CustomerEmail,
                                             "Заказ подтвержден!",
                                             createEmailBodyRule.Execute(order));
                    message.IsBodyHtml = true;



                    client.Send(message);
                }
                catch (Exception ex)
                {

                }

            }

            order.orderStatus = GuitarStarBackOffice.Shared.OrderStatus.Confirmed;
            await OrderService.UpdateOrder(order);
        }

    }
}