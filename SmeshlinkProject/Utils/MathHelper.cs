using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmeshlinkProject.Utils
{
    public static class MathHelper
    {
        #region Byte

        /// <summary>
        /// Returns a hex string representation of the given bytes array.
        /// </summary>
        /// <param name="data">The bytes array</param>
        /// <returns>The hex string representation</returns>
        public static String ToHexString(Byte[] data)
        {
            return ToHexString(data, 0, data.Length);
        }

        /// <summary>
        /// Returns a hex string representation of the given bytes array.
        /// </summary>
        /// <param name="data">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <param name="length">The length of bytes to be represented</param>
        /// <returns>The hex string representation</returns>
        public static String ToHexString(Byte[] data, Int32 offset, Int32 length)
        {
            if (data == null)
                return String.Empty;
            else
                return BitConverter.ToString(data, offset, length).Replace("-", "");
        }

        /// <summary>
        /// Converts bytes in hex string representation to a bytes array.
        /// </summary>
        /// <param name="hex">The hex string</param>
        /// <returns>The bytes array converted</returns>
        public static Byte[] ToBytes(String hex)
        {
            try
            {
                Int32 i = 0, j = 0;
                Byte[] tmp;
                if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    i = 2;
                if (hex.Length % 2 != 0)
                {
                    tmp = new Byte[(hex.Length - i) / 2 + 1];
                    tmp[j++] = Convert.ToByte(Int16.Parse(hex[i].ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture));
                    i++;
                }
                else
                {
                    tmp = new Byte[(hex.Length - i) / 2];
                }

                for (; i < hex.Length; i += 2)
                {
                    Int16 high = Int16.Parse(hex[i].ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
                    Int16 low = Int16.Parse(hex[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
                    tmp[j++] = Convert.ToByte(high * 16 + low);
                }

                return tmp;
            }
            catch { return null; }
        }

        /// <summary>
        /// Joins two bytes array together.
        /// </summary>
        /// <param name="bs1"></param>
        /// <param name="bs2"></param>
        /// <returns>The joined bytes array</returns>
        public static Byte[] JoinBytes(Byte[] bs1, Byte[] bs2)
        {
            Byte[] data = new Byte[bs1.Length + bs2.Length];
            Array.Copy(bs1, 0, data, 0, bs1.Length);
            Array.Copy(bs2, 0, data, bs1.Length, bs2.Length);
            return data;
        }

        /// <summary>
        /// Computes crc of the given bytes array.
        /// </summary>
        /// <param name="data">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <param name="len">The length of bytes to be computed</param>
        /// <returns>crc</returns>
        public static UInt16 CRC(Byte[] data, Int32 offset, Int32 len)
        {
            return CRC(0, data, offset, len);
        }

        private static UInt16 CRC(UInt16 crc, Byte[] data, Int32 offset, Int32 len)
        {
            for (Int32 i = 0; i < len; i++)
            {
                crc = CRC(crc, data[offset + i]);
            }
            return crc;
        }

        private static UInt16 CRC(UInt16 crc, Byte b)
        {
            Byte i;

            crc = (UInt16)(crc ^ b << 8);
            i = 8;
            do
            {
                if ((crc & 0x8000) > 0)
                    crc = (UInt16)(crc << 1 ^ 0x1021);
                else
                    crc = (UInt16)(crc << 1);
            }
            while (--i != 0);

            return crc;
        }

        /// <summary>
        /// Converts an integer to a bytes array in Big-Endian order.
        /// </summary>
        /// <param name="value">The integer</param>
        /// <param name="bytesLength">The length of the output bytes array</param>
        /// <returns>The bytes array converted</returns>
        public static Byte[] ToBytesB(Int32 value, Int32 bytesLength)
        {
            return ToBytes(value, bytesLength, false);
        }

        /// <summary>
        /// Converts an integer to a bytes array in Little-Endian order.
        /// </summary>
        /// <param name="value">The integer</param>
        /// <param name="bytesLength">The length of the output bytes array</param>
        /// <returns>The bytes array converted</returns>
        public static Byte[] ToBytesL(Int32 value, Int32 bytesLength)
        {
            return ToBytes(value, bytesLength, true);
        }

        /// <summary>
        /// Converts an integer to a bytes array.
        /// </summary>
        /// <param name="value">The integer</param>
        /// <param name="bytesLength">The length of the output bytes array</param>
        /// <param name="littleEndian">True if bytes in Little-Endian order</param>
        /// <returns>The bytes array converted</returns>
        public static Byte[] ToBytes(Int32 value, Int32 bytesLength, Boolean littleEndian)
        {
            Byte[] bs = new Byte[bytesLength];

            if (littleEndian)
            {
                for (Int32 i = 0; i < bytesLength; i++)
                {
                    bs[i] = (Byte)(value & 0xFF);
                    value >>= 8;
                }
            }
            else
            {
                for (Int32 i = bytesLength - 1; i >= 0; i--)
                {
                    bs[i] = (Byte)(value & 0xFF);
                    value >>= 8;
                }
            }

            return bs;
        }

        /// <summary>
        /// Converts an integer to a bytes array.
        /// </summary>
        /// <param name="value">The integer</param>
        /// <param name="bytesLength">The length of the output bytes array</param>
        /// <param name="littleEndian">True if bytes in Little-Endian order</param>
        /// <returns>The bytes array converted</returns>
        public static Byte[] ToBytes(Int64 value, Int32 bytesLength, Boolean littleEndian)
        {
            Byte[] bs = new Byte[bytesLength];

            if (littleEndian)
            {
                for (Int32 i = 0; i < bytesLength; i++)
                {
                    bs[i] = (Byte)(value & 0xFF);
                    value >>= 8;
                }
            }
            else
            {
                for (Int32 i = bytesLength - 1; i >= 0; i--)
                {
                    bs[i] = (Byte)(value & 0xFF);
                    value >>= 8;
                }
            }

            return bs;
        }

        public static Byte[] Padding(Byte[] src, Int32 len)
        {
            if (src == null)
                return new Byte[len];
            else if (src.Length == len)
                return src;
            byte[] dst = new Byte[len];
            for (int i = src.Length - 1, j = dst.Length - 1; i >= 0 && j >= 0; i--, j--)
            {
                dst[j] = src[i];
            }
            return dst;
        }

        #endregion

        #region Integer

        /// <summary>
        /// Converts bytes in Big-Endian order to a 16-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static Int16 ToInt16B(Byte[] value, Int32 offset)
        {
            return (Int16)ToInt32B(value, offset, 2);
        }

        /// <summary>
        /// Converts bytes in Big-Endian order to an unsigned 16-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static UInt16 ToUInt16B(Byte[] value, Int32 offset)
        {
            return (UInt16)ToInt32B(value, offset, 2);
        }

        /// <summary>
        /// Converts bytes in Little-Endian order to a 16-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static Int16 ToInt16L(Byte[] value, Int32 offset)
        {
            return (Int16)ToInt32L(value, offset, 2);
        }

        /// <summary>
        /// Converts bytes in Little-Endian order to an unsigned 16-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static UInt16 ToUInt16L(Byte[] value, Int32 offset)
        {
            return (UInt16)ToInt32L(value, offset, 2);
        }

        /// <summary>
        /// Converts bytes in Big-Endian order to a 32-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static Int32 ToInt32B(Byte[] value, Int32 offset)
        {
            return ToInt32B(value, offset, 4);
        }

        /// <summary>
        /// Converts bytes in Big-Endian order to an unsigned 32-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static UInt32 ToUInt32B(Byte[] value, Int32 offset)
        {
            return (UInt32)ToInt64(value, offset, 4, false);
        }

        /// <summary>
        /// Converts bytes in Little-Endian order to a 32-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static Int32 ToInt32L(Byte[] value, Int32 offset)
        {
            return ToInt32L(value, offset, 4);
        }

        /// <summary>
        /// Converts bytes in Little-Endian order to an unsigned 32-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static UInt32 ToUInt32L(Byte[] value, Int32 offset)
        {
            return (UInt32)ToInt64(value, offset, 4, true);
        }

        /// <summary>
        /// Converts bytes in Big-Endian order to a 32-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <param name="length">The length of bytes to be converted</param>
        /// <returns>The converted integer</returns>
        public static Int32 ToInt32B(Byte[] value, Int32 offset, Int32 length)
        {
            return ToInt32(value, offset, length, false);
        }

        /// <summary>
        /// Converts bytes in Little-Endian order to a 32-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <param name="length">The length of bytes to be converted</param>
        /// <returns>The converted integer</returns>
        public static Int32 ToInt32L(Byte[] value, Int32 offset, Int32 length)
        {
            return ToInt32(value, offset, length, true);
        }

        /// <summary>
        /// Converts bytes to a 32-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <param name="length">The length of bytes to be converted</param>
        /// <param name="littleEndian">True if bytes in Little-Endian order</param>
        /// <returns>The converted integer</returns>
        public static Int32 ToInt32(Byte[] value, Int32 offset, Int32 length, Boolean littleEndian)
        {
            Int32 iOutcome = 0;

            if (littleEndian)
            {
                for (Int32 i = offset + length - 1; i >= offset; i--)
                {
                    if (i < value.Length)
                        iOutcome = (iOutcome << 8) + (value[i] & 0xFF);
                }
            }
            else
            {
                for (Int32 i = 0; i < length; i++)
                {
                    if (offset + i >= value.Length)
                        break;
                    iOutcome = (iOutcome << 8) + (value[offset + i] & 0xFF);
                }
            }

            return iOutcome;
        }

        /// <summary>
        /// Converts bytes in Big-Endian order to a 64-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static Int64 ToInt64B(Byte[] value, Int32 offset)
        {
            return ToInt64(value, offset, 8, false);
        }

        /// <summary>
        /// Converts bytes in Little-Endian order to a 64-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <returns>The converted integer</returns>
        public static Int64 ToInt64L(Byte[] value, Int32 offset)
        {
            return ToInt64(value, offset, 8, true);
        }

        /// <summary>
        /// Converts bytes to a 64-bit integer.
        /// </summary>
        /// <param name="value">The bytes array</param>
        /// <param name="offset">The start position in the array</param>
        /// <param name="length">The length of bytes to be converted</param>
        /// <param name="littleEndian">True if bytes in Little-Endian order</param>
        /// <returns>The converted integer</returns>
        public static Int64 ToInt64(Byte[] value, Int32 offset, Int32 length, Boolean littleEndian)
        {
            Int64 iOutcome = 0;

            if (littleEndian)
            {
                for (Int32 i = offset + length - 1; i >= offset; i--)
                {
                    if (i < value.Length)
                        iOutcome = (iOutcome << 8) + (value[i] & 0xFF);
                }
            }
            else
            {
                for (Int32 i = 0; i < length; i++)
                {
                    if (offset + i >= value.Length)
                        break;
                    iOutcome = (iOutcome << 8) + (value[offset + i] & 0xFF);
                }
            }

            return iOutcome;
        }

        #endregion

        /// <summary>
        /// Wraps the given bytes into a TinyOS packet by appending a 2-byte CRC, prefixing with 0x7e42 and postfixing with 0x7e.
        /// </summary>
        /// <param name="data">the bytes array</param>
        public static Byte[] WrapTOSPacket(Byte[] data)
        {
            return WrapTOSPacket(data, 0, data.Length);
        }

        /// <summary>
        /// Wraps the given bytes into a TinyOS packet by appending a 2-byte CRC, prefixing with 0x7e42 and postfixing with 0x7e.
        /// </summary>
        /// <param name="data">the bytes array</param>
        /// <param name="offset">the start position in the array</param>
        /// <param name="length">the length of bytes to wrap</param>
        /// <returns>an array of wrapped bytes</returns>
        public static Byte[] WrapTOSPacket(Byte[] data, Int32 offset, Int32 length)
        {
            return WrapTOSPacket(data, offset, length, true, true);
        }

        /// <summary>
        /// Wraps the given bytes into a TinyOS packet by appending a 2-byte CRC, prefixing with 0x7e42 and postfixing with 0x7e.
        /// </summary>
        /// <param name="data">the bytes array</param>
        /// <param name="offset">the start position in the array</param>
        /// <param name="length">the length of bytes to wrap</param>
        /// <param name="hasSyncCode"></param>
        /// <param name="hasCRC"></param>
        /// <returns>an array of wrapped bytes</returns>
        public static Byte[] WrapTOSPacket(Byte[] data, Int32 offset, Int32 length, Boolean hasSyncCode, Boolean hasCRC)
        {
            Byte[] writeData = new Byte[SmeshLink.SmeshServer.SmeshDefine.SerialPacketMtu];
            Int32 currentIndex = 0;
            // 添加同步字符
            writeData[currentIndex++] = 0x7e;

            if (hasSyncCode)
            {
                // 添加数据包类型
                writeData[currentIndex++] = (Byte)SmeshLink.SmeshServer.SmeshDefine.HDLCPacketType.XPACKET_NO_ACK;
            }

            for (Int32 i = offset; i < offset + length; i++)
            {
                if (data[i] == 0x7d || data[i] == 0x7e)
                {
                    writeData[currentIndex++] = 0x7d;
                    writeData[currentIndex++] = (Byte)(data[i] ^ 0x20);
                }
                else
                {
                    writeData[currentIndex++] = data[i];
                }
            }

            if (hasCRC)
            {
                UInt16 crc;

                if (hasSyncCode)
                    crc = CRC(
                        CRC(0, (Byte)SmeshLink.SmeshServer.SmeshDefine.HDLCPacketType.XPACKET_NO_ACK),
                        data, offset, length);
                else
                    crc = MathHelper.CRC(data, offset, length);

                Byte[] crcByte = BitConverter.GetBytes(crc);
                if (crcByte[0] == 0x7d || crcByte[0] == 0x7e)
                {
                    writeData[currentIndex++] = 0x7d;
                    writeData[currentIndex++] = (Byte)(crcByte[0] ^ 0x20);
                }
                else
                {
                    writeData[currentIndex++] = crcByte[0];
                }
                if (crcByte[1] == 0x7d || crcByte[1] == 0x7e)
                {
                    writeData[currentIndex++] = 0x7d;
                    writeData[currentIndex++] = (Byte)(crcByte[1] ^ 0x20);
                }
                else
                {
                    writeData[currentIndex++] = crcByte[1];
                }
            }

            writeData[currentIndex++] = 0x7e;
            Byte[] rtndata = new Byte[currentIndex];
            Array.Copy(writeData, 0, rtndata, 0, currentIndex);
            return rtndata;
        }
    }
}
