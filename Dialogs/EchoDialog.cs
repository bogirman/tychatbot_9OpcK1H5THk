using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;
using Microsoft.Bot.Connector;
using System.Web.Http;
using System.Net;
using System.Linq;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    
    [LuisModel("cfb93013-49d1-4d5e-a433-79f324633c66", "579203d2883e4c909cc002dbe5fbfaba")]
    [Serializable]
    public class AnsTest : LuisDialog<object>
    {
        public const string Entity_location = "Location";
        //HeroCard建立
        private static Attachment GetHeroCard()
        {
            var heroCard = new HeroCard
            {
                Title = "HeroCard測試",
                Subtitle = "請問你要問的是下列哪個問題?",
                Text = "1.12312312312\r\n2.12312312312\r\n3.12312312312\r\n"
                
            };
            return heroCard.ToAttachment();
        }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //直接連LUIS
            var msg = ActivityTypes.Message;
            var luisRes = await GetIntentsEntities(msg);
            // You could choose to handle the "none" intent here, or in the bot code
            //var reply = new DataAccessLayer().GetInfo(luisRes);
            Cognitive.LUIS.LuisResult Luis = luisRes;
            var reply = Luis.Intents;
            
            string[] intent = new string[5];
            if (Luis.Intents.Count() != 0)
            {
                for (int i = 0; i < reply.Count(); i++)
                {
                    for (int j = 0; j < reply.Count()-1; j++)
                    {
                        if (reply[j].Score > reply[j].Score)
                        {
                            var temp = reply[j];
                            reply[j] = reply[j + 1];
                            reply[j + 1] = temp;
                        }
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    intent[i] = SetUT(reply[i + 1].Name);
                    //intent[i] =reply[i + 1].Name;
                }
            }

            //按鍵HEROCARD
            Activity replyToConversation =
            ShowButtons(context, "請問你要問的問題是下列何者?", intent);
            await context.PostAsync(replyToConversation);
            context.Wait(MessageReceived);
            //一般HEROCARD
            //var message = context.MakeMessage();
            //var attachment = GetHeroCard();
            //message.Attachments.Add(attachment);
            //await context.PostAsync(message);
            //context.Wait(MessageReceived);
            //一般回答
            // string message = $"不好意思，我不太了解你的問題，請問你想問的是";
            //await context.PostAsync(message);
            //context.Wait(MessageReceived);
        }

        [LuisIntent("打招呼")]
        public async Task Hello(IDialogContext context, LuisResult result)
        {
            string message = $"您好，歡迎您使用本互動機器人。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q01")]
        public async Task Tax01(IDialogContext context, LuisResult result)
        {
            string message = $"交通工具所有人或使用人。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q02")]
        public async Task Tax02(IDialogContext context, LuisResult result)
        {
            string message = $"除能舉證者外，仍由原車主繳納。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q03")]
        public async Task Tax03(IDialogContext context, LuisResult result)
        {
            string message = $"一般車輛買賣轉讓應由新舊車主共同填寫過戶申請書持向監理機關辦妥過戶手續，如車輛轉讓後，買主不辦理過戶，舊車主為確保本身權益，可以存證信函催告新車主辦理過戶手續（如新車主已行蹤不明無法通知時，可以登報方式代替）。若新車主仍不出面辦理時，可檢附存證信函或報紙2份，向監理機關辦理車輛拒不過戶註銷牌照手續。當然在註銷前，如有積欠的使用牌照稅及罰鍰等，您必須先行繳清。牌照一經註銷，車子不能再使用，您也就不會再收到使用牌照稅繳款書了。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q04")]
        public async Task Tax04(IDialogContext context, LuisResult result)
        {
            string message = $"車輛轉讓後，買方不願配合辦理過戶，原所有人為確保權益，得繳清使用牌照稅及罰鍰，並攜帶身分證（營利事業登記證）及登報2份（或存證信函），至車籍所在地監理機關辦妥車輛拒不過戶註銷手續，以後就不會再以原所有人名義課徵使用牌照稅，如有違章亦不會再以原所有人為處罰對象。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q05")]
        public async Task Tax05(IDialogContext context, LuisResult result)
        {
            string message = $"車輛失竊，除向警察機關報案外，應向公路監理機關辦理註銷牌照登記，才可免再繳納使用牌照稅。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q06")]
        public async Task Tax06(IDialogContext context, LuisResult result)
        {
            string message = $"使用大客車及貨車、小客車及機器腳踏車依照汽缸總排氣量各自有不同之稅額表(連結稅額表)。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q07")]
        public async Task Tax07(IDialogContext context, LuisResult result)
        {
            string message = $"機車汽缸總排氣量在150（含150）立方公分以下者免繳使用牌照稅。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q08")]
        public async Task Tax08(IDialogContext context, LuisResult result)
        {
            string message = $"每年四月一日起一個月內一次徵收。但營業用車輛按應納稅額，於每年四月一日及十月一日起一個月內分二次平均徵收。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q09")]
        public async Task Tax09(IDialogContext context, LuisResult result)
        {
            string message = $"每年四月一日起一個月內一次徵收。但營業用車輛按應納1.自用車、機車(151cc以上)：(1)開徵期間：每年4月1日至4月30日(2)課稅期間：當年度1月1日至12月31日 2.營業車(分上、下2期開徵)：(1)開徵期間：每年4月1日至4月30日(上期)；每年10月1日至10月31日(下期) (2)課稅期間：當年度1月1日至6月30日(上期)當年度7月1日至12月31日(下期) 稅額，於每年四月一日及十月一日起一個月內分二次平均徵收。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q10")]
        public async Task Tax10(IDialogContext context, LuisResult result)
        {
            string message = $"每年1月1日起至12月31日止應繳納的使用牌照稅，應於當年4月1日起至4月30日繳納（如果是營業用車輛可以分兩期繳納，當年4月1日起至4月30日先繳納半數，餘額於當年10月1日起至10月31日繳納）。使用牌照稅係採按日計徵稅額，去年底購買車輛時，僅繳納自購買日起至當年12月31日止的稅額，所以今年仍須繳納。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q11")]
        public async Task Tax11(IDialogContext context, LuisResult result)
        {
            string message = $"車輛為身心障礙者所有且身心障礙者領有駕駛執照，可檢附證明文件（1.身心障礙手冊或證明 2.行車執照 3.身分證）至車籍所在地之稅捐稽徵機關申辦免徵使用牌照稅，1人以1輛為限。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q12")]
        public async Task Tax12(IDialogContext context, LuisResult result)
        {
            string message = $"供身心障礙者使用之車輛，領有身心障礙手冊或證明者因身心障礙情況，致無駕駛執照，車主為其本人、配偶或同一戶籍二親等以內親屬，可檢附證明文件【1.身心障礙手冊或證明 2.行車執照 3.戶口名簿(車輛為本人或配偶所有時，得以身分證代替）】至車籍所在地之稅捐稽徵機關申辦免徵使用牌照稅，每一身障者以一輛為限。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q13")]
        public async Task Tax13(IDialogContext context, LuisResult result)
        {
            string message = $"使用牌照稅法第7條修正條文自104年1月1日起施行，增訂身心障礙者免徵使用牌照稅之二親等條件限制，即倘身障者無駕照者，車輛則限為身障者本人、配偶或同一戶籍二親等以內親屬所有。由於大伯父與您係屬三親等，不符合使用牌照稅法第7條之規定。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q14")]
        public async Task Tax14(IDialogContext context, LuisResult result)
        {
            
            string message = $"使用牌照稅法第7條修正條文自104年1月1日起施行，凡汽缸總排氣量超過2,400立方公分者，其免徵金額以2,400立方公分之稅額為限，超過部分，不予免徵。以自用小客車為例，免稅限額為新臺幣11,230元。本案自用小客車為3,000立方公分，使用牌照稅仍有應徵稅額為3,980元(15,210元-11,230元)。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q15")]
        public async Task Tax15(IDialogContext context, LuisResult result)
        {
            string message = $"車輛免稅係自稅捐稽徵機關核准日生效，使用牌照稅應計徵至核准日前一天，經核准身心障礙者免徵使用牌照稅車輛，若當年度使用牌照稅已繳納者，可辦理退還核准免稅之日起至年底之使用牌照稅。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q16")]
        public async Task Tax16(IDialogContext context, LuisResult result)
        {
            string message = $"稅捐減免及優惠部分，目前可申請免徵使用牌照稅及申報綜合所得稅時有身心障礙特別扣除額。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q17")]
        public async Task Tax17(IDialogContext context, LuisResult result)
        {
            string message = $"已立案之社會福利團體和機構，且經當地社政機關證明者，同一直轄市或縣(市)每一團體和機構最多可以申請其所有之三輛車子，免徵使用牌照稅。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q18")]
        public async Task Tax18(IDialogContext context, LuisResult result)
        {
            string message = $"免稅車輛，一經核定免稅，如其申請核定免稅之條件不變，不必每年申請核免手續。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q19")]
        public async Task Tax19(IDialogContext context, LuisResult result)
        {
            string message = $"車輛雖已廢棄，因未辦理報廢手續，使用牌照稅當然繼續課徵，現在您應儘速攜帶身分證、行車執照、車輛號牌等證件，向監理機關辦理報廢手續；或將廢棄車輛交由環保單位認可的回收清除處理機關代為處理，並攜帶上述相關證件至監理機關辦理報廢手續，使用牌照稅就可從回收日起停徵。請配合環保署廢車報廢程序，於監理機關報廢車輛時，一併辦理車輛回收，並將車輛交給合法回收商，詳情請洽資源回收專線0800-085-717(諧音「您幫我清一清」)。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q20")]
        public async Task Tax20(IDialogContext context, LuisResult result)
        {
            string message = $"依據使用牌照稅法第13條規定，交通工具所有人或使用人對已領使用牌照之交通工具，不擬使用時，應向監理機關申報停止使用，其已使用期間應納稅額，按其實際使用期間之日數計算，申報停駛期間即可不用繳納使用牌照稅，如有溢繳尚可申請退還。惟如要恢復使用時，應向監理機關辦理復駛登記。停駛之車輛，如查獲使用公共道路（含行駛或停放），依規定除責令補稅外，並處以應納稅額2倍以下之罰鍰。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q21")]
        public async Task Tax21(IDialogContext context, LuisResult result)
        {
            string message = $"車輛未如期繳納使用牌照稅，行駛公路被查獲，除責令補稅外，並處以應納稅額一倍以下之罰鍰。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q22")]
        public async Task Tax22(IDialogContext context, LuisResult result)
        {
            string message = $"機動車輛的所有人或使用人應於繳款書所載繳納期間繳納使用牌照稅，如逾期未完稅，在滯納期滿後使用公共道路（含行駛或停放）被查獲者，將被處應納稅額1倍以下的罰鍰。所以未繳稅仍繼續使用公共道路（含行駛或停放），不論係交通警察路檢或照相逕行舉發，抑或被地方稅稽徵機關查緝人員查獲，除補稅外，並按應納稅額處1倍以下的罰鍰。 機動車輛如已被監理機關註銷牌照或已報停、繳銷、註銷，經查獲使用公共道路（含行駛或停放），不論係交通警察路檢或照相逕行舉發，抑或被稅捐稽徵機關查緝人員查獲，除補稅外，再處以應納稅額2倍以下之罰鍰。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q23")]
        public async Task Tax23(IDialogContext context, LuisResult result)
        {
            string message = $"依使用牌照稅法第28條第2項規定：「報停、繳銷或註銷牌照之交通工具使用公共水陸道路經查獲者，除責令補稅外，處以應納稅額2倍以下之罰鍰。」其所謂之「使用公共水陸道路」非僅限行駛而言，在公共道路上「停車」或「臨時停車」亦屬於使用之範圍。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q24")]
        public async Task Tax24(IDialogContext context, LuisResult result)
        {
            string message = $"使用牌照稅開徵時稅單寄送地址，係先依車主於稅務局所增設之通訊地址寄送，如無設通訊地址者，則依車籍地址寄送。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q25")]
        public async Task Tax25(IDialogContext context, LuisResult result)
        {
            string message = $"各地代收稅款之金融機構皆可代收,繳款後收據至少保留5年。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        [LuisIntent("q26")]
        public async Task Tax26(IDialogContext context, LuisResult result)
        {
            string message = $"自104年4月1日起為便利雙北民眾跨區申辦使用牌照稅免、退稅案件，與臺北市稅捐稽徵處共同訂定「大臺北跨區代收代送使用牌照稅減免(退)稅申請書作業原則」，透過雙方機關代收代送方式，以節省民眾往返交通時間及成本。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        #region private static List<CardAction> CreateButtons()
        private static List<CardAction> CreateButtons(String[] st)
        {
            //LuisResult tt = new LuisResult();

            List<CardAction> cardButtons = new List<CardAction>();
            for (int i = 0; i < st.Count(); i++)
            {
                string CurrentNumber = Convert.ToString(i);
                CardAction CardButton = new CardAction()
                {
                    Type = "imBack",
                    Title = st[i],
                    Value = st[i]
                };
                cardButtons.Add(CardButton);
            }
            return cardButtons;
        }
        #endregion
        #region private static Activity ShowButtons(IDialogContext context, string strText)
        private static Activity ShowButtons(IDialogContext context, string strText,String[] st)
        {

            // Create a reply Activity
            Activity replyToConversation = (Activity)context.MakeMessage();
            replyToConversation.Text = strText;
            replyToConversation.Recipient = replyToConversation.Recipient;
            replyToConversation.Type = "message";
            // Call the CreateButtons utility method 
            // that will create 5 buttons to put on the Here Card
            List<CardAction> cardButtons = CreateButtons(st);
            // Create a Hero Card and add the buttons 
            HeroCard plCard = new HeroCard()
            {
                Buttons = cardButtons
            };
            // Create an Attachment
            // set the AttachmentLayout as 'list'
            Attachment plAttachment = plCard.ToAttachment();
            replyToConversation.Attachments.Add(plAttachment);
            replyToConversation.AttachmentLayout = "list";
            // Return the reply to the calling method
            return replyToConversation;
        }
        #endregion
        public async Task<Cognitive.LUIS.LuisResult> GetIntentsEntities(string msg)
        {
            string appId = "cfb93013-49d1-4d5e-a433-79f324633c66";
            string subscriptionKey = "579203d2883e4c909cc002dbe5fbfaba";
            bool preview = true;
            string textToPredict = msg;
            try
            {
                Cognitive.LUIS.LuisClient client = new Cognitive.LUIS.LuisClient(appId, subscriptionKey, preview);
                Cognitive.LUIS.LuisResult res = await client.Predict(textToPredict);
                return res;
            }
            catch (System.Exception exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
        public String SetUT(string entry)
        {
            string utterances = "";
            switch (entry)
            {
                case "q01":
                    utterances = "使用牌照稅的納稅義務人為誰?";
                    break;
                case "q02":
                    utterances = "車輛已轉賣他人，但未依規定辦理過戶手續，使用牌照稅應由何人繳納?";
                    break;
                case "q03":
                    utterances = "我有一部車輛，兩年前已賣給他人，可是每年還收到使用牌照稅繳款書，究竟該如何處理?";
                    break;
                case "q04":
                    utterances = "車輛如已出售，買受人如拒不過戶，如何辦理?";
                    break;
                case "q05":
                    utterances = "車輛失竊，可否免再繳納使用牌照稅?";
                    break;
                case "q06":
                    utterances = "汽車、機車每年必須繳納使用牌照稅?";
                    break;
                case "q07":
                    utterances = "車汽缸總排氣量在多少，可免繳使用牌照稅?";
                    break;
                case "q08":
                    utterances = "使用牌照稅於何時開徵?";
                    break;
                case "q09":
                    utterances = "使用牌照稅開徵期間及其課稅期間為何?";
                    break;
                case "q10":
                    utterances = "我於去年底買了一部自用小客車，於掛牌時，已繳納使用牌照稅，為何今年4月份還要再繳?";
                    break;
                case "q11":
                    utterances = "身心障礙者本人之車輛，可否申請免徵使用牌照稅?";
                    break;
                case "q12":
                    utterances = "家庭中有一人領有身心障礙手冊，其家中之車輛可否申請免徵使用牌照稅?";
                    break;
                case "q13":
                    utterances = "二等親持有身心障礙手冊，且與我同一戶籍，請問我的車是否可申請使用牌照稅免稅?";
                    break;
                case "q14":
                    utterances = "我是持有駕照的身心障礙者，名下有一輛車，已辦理免稅，為何今年還是收到使用牌照稅繳款書?";
                    break;
                case "q15":
                    utterances = "若當年度牌照稅已繳納後，辦理身障者減免牌照稅，是否可追溯其溢繳之費用，追溯期多久?";
                    break;
                case "q16":
                    utterances = "領有身心障礙手冊或證明者，繳納稅捐時有那些減免及優惠?";
                    break;
                case "q17":
                    utterances = "社會福利團體使用之車輛，可否申請免徵使用牌照稅?";
                    break;
                case "q18":
                    utterances = "經核定免稅之車輛，是否需要每年按期再申請免稅手續?";
                    break;
                case "q19":
                    utterances = "我有一部車輛，廢棄多年沒用，可是每年都收到使用牌照稅繳款書，究竟要如何處理?";
                    break;
                case "q20":
                    utterances = "我因工作關係，經常長期出國，出國期間車輛即未使用，應如何辦理?";
                    break;
                case "q21":
                    utterances = "未如期繳納使用牌照稅，行駛公路被查獲，將如何處罰?";
                    break;
                case "q22":
                    utterances = "交通違規，為何要繳納鉅額的使用牌照稅及罰鍰?";
                    break;
                case "q23":
                    utterances = "本人之汽車已被監理機關註銷牌照，因不能行駛，停放於路旁，為何有牌照稅罰鍰?";
                    break;
                case "q24":
                    utterances = "使用牌照稅開徵時稅單寄送地址為何處?";
                    break;
                case "q25":
                    utterances = "收到違章案件處分書及罰鍰繳款書, 但人在外地(非裁罰機關所在地), 要如何繳納?";
                    break;
                case "q26":
                    utterances = "我的車籍在臺北市，可否在新北市辦理使用牌照稅案件?";
                    break;
                case "打招呼":
                    utterances = "你好";
                    break;
            }

            return utterances;
        }

    }
}