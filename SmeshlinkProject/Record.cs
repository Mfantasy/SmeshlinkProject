using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmeshlinkProject
{
    class Record
    {
        #region 不通过SMS直接POST到MISTY 简单容易执行,但是不会实时更新数据流.
        //    {
        //    MistyService ms = new MistyService("api.smeshlink.com", false);
        //    ms.SetUser("mengfantong", "SMESHLINK2010");
        //        url = "http://api.smeshlink.com/mengfantong/81EAA500E524224A.json";
        //        using (Stream retval = HttpUtil.HttpGet(url))
        //        {
        //            JsonFormatter jfmt = new JsonFormatter();
        //    var feed = jfmt.ParseFeed(retval);
        //    Console.WriteLine(feed);
        //            BurnNow(feed, ms);
        //    //ms.
        //}

        //private void BurnNow(Feed feed, MistyService ms)
        //    {
        //        try
        //        {
        //            if (!ms.Feed().Update(CopyValueOnly(feed)))
        //                MessageBox.Show("FeedBurner: update failed");
        //        }
        //        catch (ServiceException e)
        //        {
        //            MessageBox.Show(e.Message);
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show(e.Message);
        //        }
        //    }
        //    private static Feed CopyValueOnly(Feed feed)
        //    {
        //        Feed target = new Feed();
        //        target.Id = feed.Id;
        //        target.Name = feed.Name;
        //        target.ParentId = feed.ParentId;
        //        //target.Updated = feed.Updated;
        //        target.Updated = DateTime.Now;
        //        target.CurrentValue = feed.CurrentValue;
        //        //target.EntriesMap = feed.EntriesMap;

        //        foreach (Feed child in feed.Children)
        //        {
        //            target.AddChild(CopyValueOnly(child));
        //        }

        //        return target;
        //    }
        #endregion
    }
}
