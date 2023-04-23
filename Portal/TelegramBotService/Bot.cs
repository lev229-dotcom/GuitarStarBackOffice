using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using System.Text.Json;

namespace Portal.TelegramBotService;

public class Bot : BackgroundService
{
    private readonly ITelegramBotClient bot;

    private readonly ConfirmOrderCommand confirmOrderCommand;


    public Bot(ITelegramBotClient bot, ConfirmOrderCommand confirmOrderCommand)
    {
        this.bot = bot;
        this.confirmOrderCommand = confirmOrderCommand;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {

        //Если произошло нажатие по кнопке
        if (update.Type == UpdateType.CallbackQuery)
        {
            CallbackQuery callbackQuery = update.CallbackQuery;

            await confirmOrderCommand.Execute(callbackQuery);
        }

    }

    //Вывод в консоль сообщений об ошибках
    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Некоторые действия
        var error = JsonSerializer.Serialize(exception);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };

        bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);

        //while (!stoppingToken.IsCancellationRequested)
        //{

        //    await Task.Delay(1000, stoppingToken);
        //}
    }
}
