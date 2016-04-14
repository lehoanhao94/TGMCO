using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TGMCO.Models
{
    public class StringRandom
    {
        private static Random m_Random = null;
        private static int m_Size = int.MinValue;

        public StringRandom()
        {
            try
            {
                m_Size = 30;
                m_Random = new Random((int)DateTime.Now.Ticks);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RandomString()
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                char ch;
                for (int i = 0; i < m_Size; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * m_Random.NextDouble() + 65)));
                    builder.Append(ch);
                }

                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}