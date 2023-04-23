using GuitarStarBackOffice.Shared;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Portal.TelegramBotService;

public class SendNewOrderMessageCommand
{
    private readonly ITelegramBotClient bot;
    private readonly Order order;

    public SendNewOrderMessageCommand(ITelegramBotClient bot, Order order)
    {
        this.bot = bot;
        this.order = order;
    }

    public async Task SendNewOrdersAsync()
    {
        var confirmButton = InlineKeyboardButton.WithCallbackData("Подтвердить", order.IdOrder.ToString());

        InlineKeyboardMarkup inline = new InlineKeyboardMarkup(confirmButton);

        var messageBuilder = new StringBuilder();
        messageBuilder
            .AppendLine($"🆕 <b>Новый заказ!</b>")
            .AppendLine($"<b>Номер заказа</b> #{order.OrderNumber}")
            .AppendLine($"<u>Контактные данные клиента</u>")
            .AppendLine($"<b>Имя клиента:</b> {order.CustomerName}");

        if (order.OrderNumber is not 0 && order.CustomerNumber.Length > 0)
            messageBuilder.AppendLine($"<b>Номер телефона:</b> {order.CustomerNumber}");

        messageBuilder
            .AppendLine($"<b>Электронная почта</b> {order.CustomerEmail}")
            .AppendLine($"<b>Адрес</b> {order.CustomerAddress}");

        await bot.SendTextMessageAsync(-748847994, messageBuilder.ToString(), ParseMode.Html,
                                       replyMarkup: inline);
        if(order.FileOrder.Data is not null)
        {
            using (var stream = new MemoryStream(Convert.FromBase64String(order.FileOrder.Data)))
            {
                InputOnlineFile iof = new(stream)
                {
                    FileName = $"Заказ # {order.OrderNumber}.pdf"
                };
                await bot.SendDocumentAsync(-748847994, iof);
            }

        }

    }
}
