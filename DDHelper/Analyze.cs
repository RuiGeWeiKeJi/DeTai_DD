using System . Collections . Generic;
using Newtonsoft . Json;

namespace DDHelper
{
    public class Analyze
    {

        /// <summary>
        /// GET请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public static T Get<T> ( string requestUrl ) where T : ResultPackage, new()
        {
            string resultJson = RequestHelper . Get ( requestUrl );
            return AnalyzeResult<T> ( resultJson );
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="requestParamOfJsonStr"></param>
        /// <returns></returns>
        public static T POST<T> ( string requestUrl ,string requestParamOfJsonStr ) where T : ResultPackage, new()
        {
            string resultJson = RequestHelper . Post ( requestUrl ,requestParamOfJsonStr );
            return AnalyzeResult<T> ( resultJson );
        }

        /// <summary>
        /// 分析结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultJson"></param>
        /// <returns></returns>
        public static T AnalyzeResult<T> ( string resultJson ) where T : ResultPackage, new()
        {
            ResultPackage tempResult = null;
            if ( !string . IsNullOrEmpty ( resultJson ) )
            {
                tempResult = JsonConvert . DeserializeObject<ResultPackage> ( resultJson );
            }
            T result = null;
            if ( tempResult != null && tempResult . IsOK ( ) )
            {
                result = JsonConvert . DeserializeObject<T> ( resultJson );
            }
            else if ( tempResult != null )
            {
                result = tempResult as T;
            }
            else if ( tempResult == null )
            {
                result = new T ( );
            }

            result . Json = resultJson;
            return result;
        }

        /// <summary>
        /// POST机器人请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiurl"></param>
        /// <param name="jsonString"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T POST<T> ( string apiurl ,string jsonString ,Dictionary<string ,string> headers = null ) where T : ResultPackage, new()
        {
            string resultJson = RequestHelper . Post ( apiurl ,jsonString ,headers );
            return AnalyzeResult<T> ( resultJson );
        }

    }
}
