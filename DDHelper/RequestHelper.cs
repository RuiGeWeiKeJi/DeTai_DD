using System . Collections . Generic;
using System . IO;
using System . Net;
using System . Text;

namespace DDHelper
{
    public class RequestHelper
    {
        /// <summary>
        /// 执行基本的命令方法,以GET方法
        /// </summary>
        /// <param name="apiurl"></param>
        /// <returns></returns>
        public static string Get ( string apiurl )
        {
            WebRequest request = WebRequest . Create ( @apiurl );
            request . Method = "GET";
            WebResponse response = request . GetResponse ( );
            Stream stream = response . GetResponseStream ( );
            Encoding encode = Encoding . UTF8;
            StreamReader reader = new StreamReader ( stream ,encode );
            string resultJson = reader . ReadToEnd ( );
            return resultJson;
        }

        /// <summary>
        /// 以post方式提交命令
        /// </summary>
        /// <param name="apiurl"></param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static string Post ( string apiurl ,string jsonString )
        {
            WebRequest requset = WebRequest . Create ( @apiurl );
            requset . Method = "POST";
            requset . ContentType = "application/json";

            byte [ ] bs = Encoding . UTF8 . GetBytes ( jsonString );
            requset . ContentLength = bs . Length;
            Stream newStream = requset . GetRequestStream ( );
            newStream . Write ( bs ,0 ,bs . Length );
            newStream . Close ( );

            WebResponse response = requset . GetResponse ( );
            Stream stream = response . GetResponseStream ( );
            Encoding encode = Encoding . UTF8;
            StreamReader reader = new StreamReader ( stream ,encode );
            string resultJson = reader . ReadToEnd ( );
            return resultJson;
        }

        /// <summary>
        /// 以POST方式提交机器人消息
        /// </summary>
        /// <param name="apiurl">请求的URL</param>
        /// <param name="jsonString">请求的json参数</param>
        /// <param name="headers">请求的key-value字典</param>
        /// <returns></returns>
        public static string Post ( string apiurl ,string jsonString ,Dictionary<string ,string> headers = null )
        {
            WebRequest request = WebRequest . Create ( @apiurl );
            request . Method = "POST";
            request . ContentType = "application/json";
            if ( headers != null )
            {
                foreach ( var keyValue in headers )
                {
                    if ( keyValue . Key == "Content-Type" )
                    {
                        request . ContentType = keyValue . Value;
                        continue;
                    }
                    request . Headers . Add ( keyValue . Key ,keyValue . Value );
                }
            }

            if ( string . IsNullOrEmpty ( jsonString ) )
            {
                request . ContentLength = 0;
            }
            else
            {
                byte [ ] bs = Encoding . UTF8 . GetBytes ( jsonString );
                request . ContentLength = bs . Length;
                Stream newStream = request . GetRequestStream ( );
                newStream . Write ( bs ,0 ,bs . Length );
                newStream . Close ( );
            }

            WebResponse response = request . GetResponse ( );
            Stream stream = response . GetResponseStream ( );
            Encoding encode = Encoding . UTF8;
            StreamReader reader = new StreamReader ( stream ,encode );
            string resultJson = reader . ReadToEnd ( );
            return resultJson;
        }
    }
}
