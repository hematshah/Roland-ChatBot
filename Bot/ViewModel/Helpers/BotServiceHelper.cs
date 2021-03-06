﻿using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bot.ViewModel.Helpers
{
    public class BotServiceHelper
    {
        public Conversation _Conversation
        {
            get;
            set;
        }

        public BotServiceHelper()
        {
            CreateConversation();
        }

        private async void CreateConversation()
        {
            string endpoint = "/v3/directline/conversations";

            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://directline.botframework.com");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer gaCIRrdSsIw.HCZP1dJfnWOvNHO4KkexuDy9FY2yQivDYctMAd-KpaY");

                var response = await client.PostAsync(endpoint, null);
                string json = await response.Content.ReadAsStringAsync();

                _Conversation = JsonConvert.DeserializeObject<Conversation>(json);
            }
        }


        public async void SubmitActivity(string messages) 
        {
            string endpoint = $"/v3/directline/conversations/{_Conversation.ConversationId}/activities";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://directline.botframework.com");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer gaCIRrdSsIw.HCZP1dJfnWOvNHO4KkexuDy9FY2yQivDYctMAd-KpaY");

                Activity activity = new Activity
                {
                    From = new BotAccount
                    {
                        Id = "User1"
                    },

                    Text = messages,

                    Type = "messages"

                };


                string jsonContents = JsonConvert.SerializeObject(activity);
                var buffer = Encoding.UTF8.GetBytes(jsonContents);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


                var response = await client.PostAsync(endpoint, byteContent);
                string json = await response.Content.ReadAsStringAsync();

                var obj = JObject.Parse(json);

                string id = (string)obj.SelectToken("id");
            }

        }

        public class BotAccount 
        {
            public string Id { get; set; }

            public string Name 
            {
                get;
                set;
            }
        }

        public class Activity 
        {
            public BotAccount From 
            {
                get;
                set;
            }

            public string Text 
            {
                get;
                set;
            }

            public string Type { get; set; }
        }

        public class Conversation
        {
            public string ConversationId
            {
                get;
                set;
            }

            public string Token
            {
                get;
                set;
            }

            public string StreamUrl
            {
                get;
                set;
            }

            public string ReferenceGrammarId
            {
                get;
                set;
            }

            public int Expires_in
            {
                get;
                set;
            }
        }
    }
}
