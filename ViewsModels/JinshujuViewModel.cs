﻿using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OGSToolBox.ViewsModels
{
    public class JinshujuViewModel
    {
        public class CreatePublishedFormEntry
        {
            /// <summary>
            /// 
            /// </summary>
            public string errors { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string redirectUrl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string wxOnetimeMessageRedirectUrl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cookies { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string redirectToMiniprogramSuccess { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string submitterExists { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string identityFieldLabel { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string __typename { get; set; }
        }
        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public CreatePublishedFormEntry createPublishedFormEntry { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
        }



        public string Id { get; set; } = String.Empty;
        public string StudentName { get; set; }
        public string ClassId { get; set; }
        public string StudentNum { get; set; }
        public string TeacherName { get; set; }

        public Command Submit { get; set; }
        public JinshujuViewModel()
        {
            Submit = new Command(DoSubmitAsync);
        }

        private async void DoSubmitAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://jinshuju.net/graphql/f/" + this.Id),
                Headers =
    {
        { "User-Agent", "OGSToolBox" },
    },
                Content = new StringContent("{\r\n\t\"operationName\": \"CreatePublishedFormEntry\",\r\n\t\"variables\": {\r\n\t\t\"input\": {\r\n\t\t\t\"formId\": \"" + this.Id + "\",\r\n\t\t\t\"entryAttributes\": {\r\n\t\t\t\t\"field_1\": \"" + this.StudentName + "\",\r\n\t\t\t\t\"field_2\": \"" + this.ClassId + "\",\r\n\t\t\t\t\"field_3\": \"" + this.StudentNum + "\",\r\n\t\t\t\t\"field_4\": \"" + this.TeacherName + "\"\r\n\t\t\t},\r\n\t\t\t\"captchaData\": null,\r\n\t\t\t\"weixinAccessToken\": null,\r\n\t\t\t\"xFieldWeixinOpenid\": null,\r\n\t\t\t\"weixinInfo\": null,\r\n\t\t\t\"prefilledParams\": \"\",\r\n\t\t\t\"embedded\": false,\r\n\t\t\t\"internal\": false,\r\n\t\t\t\"backgroundImage\": false,\r\n\t\t\t\"formMargin\": false,\r\n\t\t\t\"hasPreferential\": false,\r\n\t\t\t\"fillingDuration\": 1,\r\n\t\t\t\"forceSubmit\": false\r\n\t\t}\r\n\t},\r\n\t\"extensions\": {\r\n\t\t\"persistedQuery\": {\r\n\t\t\t\"version\": 1,\r\n\t\t\t\"sha256Hash\": \"0f9106976e7cf5f19e8878877bc8030cddcb7463dd76f4e02bc5c67b5874eeae\"\r\n\t\t}\r\n\t}\r\n}")
                {
                    Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
                }
            };
            using var response = await client.SendAsync(request);
            string body = await response.Content.ReadAsStringAsync();
            if (body == "{\"errors\":[{\"message\":\"表单不存在\"}]}")
            {
                await Toast.Make("表单编号错误", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            }
            else if (this.Id == String.Empty)
            {
                await Toast.Make("请输入表单编号", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            }
            else
            {
                Root rt = JsonConvert.DeserializeObject<Root>(body);
                string rs = rt.data.createPublishedFormEntry.redirectUrl;
                await Toast.Make("提交成功，成功链接:\n" + rs, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            }
        }
    }
}