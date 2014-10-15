using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getLocationByBaidu
{
    class Scenic : IComparable
    {
        public string scenicName
        {
            get;
            set;
        }
        public string scenicLng
        {
            get;
            set;
        }
        public string scenicLat
        {
            get;
            set;
        }
        public string comeFrom
        {
            get;
            set;
        }
        public int CompareTo(object obj)
        {
            int result;
            try
            {
                Scenic scenic = obj as Scenic;
                result = this.scenicName.CompareTo(scenic.scenicName);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                throw new Exception(e.Message);
            }
        }
    }
}
