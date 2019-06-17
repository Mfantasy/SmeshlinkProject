using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmeshlinkProject.DAL
{
    class DataProcess
    {
        public Stream RetrieveByMistyAPI(DateTime start, DateTime end, string gatewayId)
        {
            string timeStart = start.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
            string timeEnd = end.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
            string strUrl = String.Format("http://api.smeshlink.com/feeds/{0}.xml?key=df44011f-3daf-4372-8c14-9ad29a63a5cb&start={1}&end={2}", gatewayId, timeStart, timeEnd);
            Stream result = HttpUtil.HttpGet(strUrl);
            return result;
        }
    }
}
