using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Web.Http.Description;
using System.Net.Http;
using System;
using System.Linq;
using Microsoft.Cognitive.LUIS;
using System.Net;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// receive a message from a user and send replies
        /// </summary>
        /// <param name="activity"></param>
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {


            // check if activity is of type message
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                //呼LUIS
                await Conversation.SendAsync(activity, () => new AnsTest());

                //直接連LUIS
                //var msg = ActivityTypes.Message;
                //var luisRes = await GetIntentsEntities(msg);
                //// You could choose to handle the "none" intent here, or in the bot code
                ////var reply = new DataAccessLayer().GetInfo(luisRes);
                //Cognitive.LUIS.LuisResult Luis = luisRes;
                //var reply = Luis.Intents;
                //string intent = "";
                //if (Luis.Intents.Count() != 0)
                //{
                //    for (int i = 0; i < Luis.Intents.Count(); i++)
                //    {
                //        if (reply[i].Score > 0.5)
                //        {
                //            intent = intent + "," + reply[i].Name;
                //        }

                //    }
                //}
                //var ans = activity.CreateReply(intent);
                //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //await connector.Conversations.ReplyToActivityAsync(ans);
                //var resp = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
                //resp.Content = new StringContent(msg, System.Text.Encoding.UTF8, "text/plain");
                //return resp;
            }
            else
            {
                HandleSystemMessage(activity);
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);



        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                if (message.MembersAdded.Any(o => o.Id == message.Recipient.Id))
                {
                    var RootDialog_Welcome_Message = "你好，我是牌照稅小助手，很高興為您服務，您可以輸入任何文字，來詢問我關於牌照稅的問題。";
                    var reply = message.CreateReply(RootDialog_Welcome_Message);
                    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                    connector.Conversations.ReplyToActivityAsync(reply);
                }
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
        public async Task<Cognitive.LUIS.LuisResult> GetIntentsEntities(string msg)
        {
            string appId = "cfb93013-49d1-4d5e-a433-79f324633c66";
            string subscriptionKey = "579203d2883e4c909cc002dbe5fbfaba";
            bool preview = true;
            string textToPredict = msg;
            try
            {
                LuisClient client = new LuisClient(appId, subscriptionKey, preview);
                Cognitive.LUIS.LuisResult res = await client.Predict(textToPredict);
                return res;
            }
            catch (System.Exception exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

    }
}