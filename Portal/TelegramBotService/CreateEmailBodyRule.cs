using GuitarStarBackOffice.Shared;
using System.Diagnostics.Metrics;
using System.Text;

namespace Portal.TelegramBotService
{
    public class CreateEmailBodyRule
    {
        public string Execute(Order order)
        {
            #region Email
            // С начала письма до таблицы
            string header = @$"<!DOCTYPE html>
<html lang=""en"">

<head>
   <meta charset=""UTF-8"">
   <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
   <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
   <title>ITALZ</title>
   <link rel=""preconnect"" href=""https://fonts.googleapis.com"">
   <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
   <link href=""https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,700;1,400&display=swap"" rel=""stylesheet"">

</head>

<body style=""background-color: #efefef; margin: 0; padding: 0; width: 100%; border: 0; -webkit-text-size-adjust: 100%;
   -ms-text-size-adjust: 100%;"">
   <table data-tilda-email=""yes"" data-tilda-project-id=""2483180"" data-tilda-page-id=""11435640""
      data-tilda-page-alias=""touch-1"" cellpadding=""0"" cellspacing=""0""
      style=""border-collapse: separate;  margin: 0 auto;  max-width: 600px;width: 100%; padding: 30px 15px; font-family: 'Roboto', Arial, sans-serif;"">
      <tr style=""padding-bottom: 30px;"">
         <td>
            <table cellpadding=""0"" cellspacing=""0"">

               <tr>
                  <td>
                     <table style=""background-color:#f8f8f8; border-collapse: separate; margin: 0; display: block;
                        padding: 30px 30px;
                        width: 100%;"">
                        <tr>
                           <td>
                              <table cellpadding=""0"" cellspacing=""0"" data-record-type=""323""
                                 style=""padding-bottom: 15px; border-collapse: separate; margin: 0;"">
                                 <tr>
                                    <td
                                       style=""color:#222222; font-size:18px; line-height: 1.2; font-family: 'Roboto', Arial, sans-serif;"">
                                        Для вашего заказа требуется оплата.
                                        Убедитесь, что заказ сформирован правильно, и закончите оформление в ближайшее время.
                                    </td>
                                 </tr>
                              </table>
                           </td>
                        </tr>
                     </table>
                  </td>
               </tr>

            </table>
         </td>
      </tr>
      <tr>
         <td style=""padding: 3px 0""></td>
      </tr>
      <tr>
         <td>
            <table cellpadding=""0"" cellspacing=""0"">

               <tr>
                  <td>
                     <table style=""background-color:#f8f8f8; border-collapse: separate; margin: 0; display: block;
                        padding: 30px 30px;
                        width: 100%;"">
                        <tr>
                           <td>
                              <table cellpadding=""0"" cellspacing=""0"" data-record-type=""323""
                                 style=""padding-bottom: 15px; border-collapse: separate; margin: 0;"">
                                 <tr>
                                    <td
                                       style=""color:#222222; font-size:20px; line-height: 1.2; font-family: 'Roboto', Arial, sans-serif;"">
                                       Добрый день, {order.CustomerName}!
                                    </td>
                                 </tr>
                              </table>
                           </td>
                        </tr>
                        <tr>
<td>
                              <table style=""padding-bottom: 15px; border-collapse: separate; margin: 0;"">
                                 <tr>
                                    <td
                                       style=""color:#444444; font-size: 16px; line-height:1.45; font-family: 'Roboto', Arial, sans-serif;"">
                                       Ваш заказ необходимо оплатить.<br />
                                       Вы можете это сделать, перейдя по ссылке в письме. Если у вас есть какие-либо вопросы или замечения, пожалуйста
                                       , свяжитесь с нами по контактам, которые были указаны в электорнном письме.
                                    </td>
                                 </tr>
                              </table>
                           </td>
                        </tr>
                        <tr>
                           <td>
                              <table cellpadding=""0"" cellspacing=""0"" data-record-type=""323""
                                 style=""padding-bottom: 15px; border-collapse: separate; margin: 0;"">
                                 <tr>
                                    <td
                                       style=""color:#222222; font-size:16px; line-height: 1.2; font-family: 'Roboto', Arial, sans-serif;"">
                                        Проверьте правильность заказа и закончите оформление
                                    </td>
                                 </tr>
                              </table>
                           </td>
                        </tr>
                             <tr>
                           <td>
                              <table cellpadding=""0"" cellspacing=""0"" data-record-type=""323""
                                 style=""padding: 15px 10px; border-collapse: separate; margin: 0; border: 1px solid #444444;  width: 100%;"">";

            //
            // Заключительная часть таблицы и до конца
            //
            string footer = $@"
                                <tr>
                                    <td style=""border-top: 1px solid #222222;padding-bottom: 5px;"">
                                    </td>
                                    <td style=""border-top: 1px solid #222222;"">
                                    </td>
                                 </tr>
                                 <tr>
                                    <td style="" color:#222222; font-size:18px; line-height: 1.2; font-family: 'Roboto' ,
                                       Arial, sans-serif;font-weight:bold;"">
                                       Общая стоимость</td>
                                    <td
                                       style=""color:#3167eb; font-size:18px; line-height: 1.2; font-family: 'Roboto', Arial, sans-serif;"">
                                       {order.TotalOrderAmount} ₽
                                    </td>
                                 </tr>
                              </table>
                           </td>
                        </tr>
 <tr>
                           <td style="" width: 100%;"">
                              <table style=""padding: 10px 0; min-width: 220px;"">
                                 <tr>
                                    <td
                                       style=""text-align:center; border: 1px solid #3167eb; border-radius: 4px; display: block; padding: 13px 15px;"">
                                       <a href=""https://localhost:7066/api/order/DownloadLayoutElementsFile/{order.FileOrderId}"" скачать
                                          style=""border-radius: 4px;  text-decoration: none;
                                                display:inline-block; font-family: 'Roboto', Arial, sans-serif; color: #3167eb; font-size: 16px; margin: 0"">
                                          Бланк заказа
                                       </a>
                                    </td>
                                 </tr>
                              </table>
                           </td>
                        </tr>
                        <tr>
                           <td style="" width: 100%;"">
                              <table style="" min-width: 220px;"">
                                 <tr>
                                    <td style="" text-align:center; background-color: #3167eb; border-radius: 4px;
                                 display: block; padding: 13px 15px;"">
                                       <a href=""https://localhost:7066/api/order/PayOrder/{order.IdOrder}"" target=""_blank""
                                          style=""border-radius: 4px;  text-decoration: none;
                                                display:inline-block; font-family: 'Roboto', Arial, sans-serif; color: white; font-size: 16px; margin: 0"">
                                          Оплатить
                                       </a>
                                    </td>
                                 </tr>
                              </table>
                           </td>
                        </tr>

                     </table>
                  </td>
               </tr>

            </table>
         </td>
      </tr>
      <tr>
         <td style=""padding: 3px 0;""></td>
      </tr>
      <tr>
         <td>
            <table
               style=""border-collapse: separate;  margin: 0; padding: 40px 30px; background-color: #f8f8f8; width: 100%;"">

               <tr>
                  <td style=""padding-bottom: 15px;"">
                     <a href=""https://electro-star.ru"" target=""_blank""
                        style=""font-size:0;text-decoration: none; border:solid #ffffff 0px; outline:solid #ffffff 0px"">
                        <img src=""https://clipground.com/images/blue-star-logo-3.png""
                           alt=""electro-star"" title=""electro-star""  height=""60"" />
                     </a>
                  </td>
               </tr>
               <tr>
                  <td>
                     <p style=""color:#222222;font-size:18px; font-family: 'Roboto', Arial, sans-serif; margin: 0;"">
                        Electro-Star официальный сайт:
                        <a href=""https://electro-star.ru""
                           style=""color:#3167eb;font-size:18px;line-height:1.3; ; text-decoration: none; font-family: 'Roboto', Arial, sans-serif;"">
                           electro-star.ru </a>
                     </p>

                     <p style=""color:#222222;font-size:18px; font-family: 'Roboto', Arial, sans-serif; margin: 0;"">
                        Contacteer ons:
                        <a href=""tel:+7 495 570 00 57 ""
                           style=""color:#3167eb;font-size:18px;line-height:1.3;  text-decoration: none; font-family: 'Roboto', Arial, sans-serif;"">
                           +7 495 570 00 57 </a>
                     </p>
                  </td>
               </tr>
            </table>
         </td>
      </tr>

   </table>
</body>

</html>
";
            #endregion


            // Таблица
            StringBuilder mail = new();
            mail.Append(header);

            int linesProcessed = 0;
            int count = order.OrderElements.Count();

            foreach (var element in order.OrderElements)
            {
                //Подсчет стоимости по элементам
                double layoutElementValue = element.Product.ProductPrice * element.ElementsCount;

                mail.Append(
                    $@"
                                <tr>
                                    <td
                                       style=""padding-bottom: 10px;color:#222222; font-size:18px; line-height: 1.2; font-family: 'Roboto', Arial, sans-serif; font-weight:bold;"">
                                       {element.Product.ProductName}
                                    </td>
                                    <td
                                       style=""padding-bottom: 10px;color:#3167eb; font-size:18px; line-height: 1.2; font-family: 'Roboto', Arial, sans-serif;"">
                                       {Math.Round(layoutElementValue, 2)}  ₽
                                    </td>
                                 </tr>
                                <tr>
                                    <td
                                       style=""padding-bottom: 5px;color:#444444; font-size:14px; line-height: 1.2; font-family: 'Roboto', Arial, sans-serif;"">
                                       {element.Product.Category.CategoryName}
                                    </td>
                                 </tr>");
                if (linesProcessed != count - 1)
                {
                    mail.Append(@"<tr>
                                    <td style=""border-top: 1px solid #BBBBBB;padding-bottom: 5px;"">
                                    </td>
                                    <td style=""border-top: 1px solid #BBBBBB;"">
                                    </td>
                                </tr>");
                }

                linesProcessed++;


            }


            return mail.Append(footer).ToString();

        }
    }
}