﻿/*
 * This file is subject to the terms and conditions defined in
 * file 'license.txt', which is part of this source code package.
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Management;

namespace SteamKit2
{
    static class Utils
    {
        public static DateTime DateTimeFromUnixTime( uint unixTime )
        {
            DateTime origin = new DateTime( 1970, 1, 1, 0, 0, 0, 0 );
            return origin.AddSeconds( unixTime );
        }

        public static string EncodeHexString(byte[] input)
        {
            return input.Aggregate(new StringBuilder(),
                       (sb, v) => sb.Append(v.ToString("x2"))
                      ).ToString();
        }

        public static byte[] DecodeHexString(string hex)
        {
            if (hex == null)
                return null;

            int chars = hex.Length;
            byte[] bytes = new byte[chars / 2];

            for (int i = 0; i < chars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }

        public static EOSType GetOSType()
        {
            var osVer = Environment.OSVersion;
            var ver = osVer.Version;

            switch ( osVer.Platform )
            {
                case PlatformID.Win32Windows:
                    {
                        switch ( ver.Minor )
                        {
                            case 0:
                                return EOSType.Win95;

                            case 10:
                                return EOSType.Win98;

                            case 90:
                                return EOSType.WinME;

                            default:
                                return EOSType.WinUnknown;
                        }
                    }

                case PlatformID.Win32NT:
                    {
                        switch ( ver.Major )
                        {
                            case 4:
                                return EOSType.WinNT;

                            case 5: // XP family
                                if ( ver.Minor == 0 )
                                    return EOSType.Win200;

                                if ( ver.Minor == 1 || ver.Minor == 2 )
                                    return EOSType.WinXP;

                                goto default;

                            case 6: // Vista & 7
                                if ( ver.Minor == 0 )
                                    return EOSType.WinVista;

                                if ( ver.Minor == 1 )
                                    return EOSType.Win7;

                                goto default;

                            default:
                                return EOSType.WinUnknown;
                        }
                    }

                case PlatformID.Unix:
                    return EOSType.LinuxUnknown; // this _could_ be mac, but we're gonna just go with linux for now

                default:
                    return EOSType.Unknown;
            }
        }


        public static byte[] GenerateMachineID()
        {
            // this is steamkit's own implementation, it doesn't match what steamclient does
            // but this should make it so individual systems can be identified

            PlatformID platform = Environment.OSVersion.Platform;

            if ( platform == PlatformID.Win32NT )
            {
                StringBuilder hwString = new StringBuilder();

                try
                {
                    using ( ManagementClass procClass = new ManagementClass( "Win32_Processor" ) )
                    {
                        foreach ( var procObj in procClass.GetInstances() )
                        {
                            hwString.AppendLine( procObj[ "ProcessorID" ].ToString() );
                        }
                    }
                }
                catch { }

                try
                {
                    using ( ManagementClass hdClass = new ManagementClass( "Win32_LogicalDisk" ) )
                    {
                        foreach ( var hdObj in hdClass.GetInstances() )
                        {
                            string hdType = hdObj[ "DriveType" ].ToString();

                            if ( hdType != "3" )
                                continue; // only want local disks

                            hwString.AppendLine( hdObj[ "VolumeSerialNumber" ].ToString() );
                        }
                    }
                }
                catch { }

                try
                {
                    using ( ManagementClass moboClass = new ManagementClass( "Win32_BaseBoard" ) )
                    {
                        foreach ( var moboObj in moboClass.GetInstances() )
                        {
                            hwString.AppendLine( moboObj[ "SerialNumber" ].ToString() );
                        }
                    }
                }
                catch { }


                try
                {
                    return CryptoHelper.SHAHash( Encoding.ASCII.GetBytes( hwString.ToString() ) );
                }
                catch { return null; }
            }
            else
            {
                // todo: implement me!
                return null;
            }
        }

        public static T[] GetAttributes<T>( this Type type, bool inherit = false )
            where T : Attribute
        {
            return type.GetCustomAttributes( typeof( T ), inherit ) as T[];
        }
    }

    static class MsgUtil
    {
        private const uint ProtoMask = 0x80000000;
        private const uint EMsgMask = ~ProtoMask;

        public static EMsg GetMsg( uint integer )
        {
            return ( EMsg )( integer & EMsgMask );
        }

        public static EGCMsg GetGCMsg( uint integer )
        {
            return ( EGCMsg )( integer & EMsgMask );
        }

        public static EMsg GetMsg( EMsg msg )
        {
            return GetMsg( ( uint )msg );
        }
        public static EGCMsg GetGCMsg( EGCMsg msg )
        {
            return GetGCMsg( ( uint )msg );
        }

        public static bool IsProtoBuf( uint integer )
        {
            return ( integer & ProtoMask ) > 0;
        }

        public static bool IsProtoBuf( EMsg msg )
        {
            return IsProtoBuf( ( uint )msg );
        }

        public static bool IsProtoBuf( EGCMsg msg )
        {
            return IsProtoBuf( ( uint )msg );
        }

        public static EMsg MakeMsg( EMsg msg )
        {
            return msg;
        }

        public static EGCMsg MakeGCMsg( EGCMsg msg )
        {
            return msg;
        }

        public static EMsg MakeMsg( EMsg msg, bool protobuf )
        {
            if ( protobuf )
                return ( EMsg )( ( uint )msg | ProtoMask );

            return msg;
        }

        public static EGCMsg MakeGCMsg( EGCMsg msg, bool protobuf )
        {
            if ( protobuf )
                return ( EGCMsg )( ( uint )msg | ProtoMask );

            return msg;
        }
    }

    static class WebHelpers
    {
        static bool IsUrlSafeChar( char ch )
        {
            if ( ( ( ( ch >= 'a' ) && ( ch <= 'z' ) ) || ( ( ch >= 'A' ) && ( ch <= 'Z' ) ) ) || ( ( ch >= '0' ) && ( ch <= '9' ) ) )
            {
                return true;
            }

            switch ( ch )
            {
                case '-':
                case '.':
                case '_':
                    return true;
            }

            return false;
        }

        public static string UrlEncode( string input )
        {
            return UrlEncode( Encoding.UTF8.GetBytes( input ) );
        }


        public static string UrlEncode( byte[] input )
        {
            StringBuilder encoded = new StringBuilder( input.Length * 2 );

            for ( int i = 0 ; i < input.Length ; i++ )
            {
                char inch = ( char )input[ i ];

                if ( IsUrlSafeChar( inch ) )
                {
                    encoded.Append( inch );
                }
                else if ( inch == ' ' )
                {
                    encoded.Append( '+' );
                }
                else
                {
                    encoded.AppendFormat( "%{0:X2}", input[ i ] );
                }
            }

            return encoded.ToString();
        }
    }

    static class NetHelpers
    {
        public static IPAddress GetLocalIP(Socket activeSocket)
        {
            IPEndPoint ipEndPoint = activeSocket.LocalEndPoint as IPEndPoint;

            if ( ipEndPoint == null || ipEndPoint.Address == IPAddress.Any )
                throw new Exception( "Socket not connected" );

            return ipEndPoint.Address;
        }

        public static IPAddress GetIPAddress( uint ipAddr )
        {
            byte[] addrBytes = BitConverter.GetBytes( ipAddr );
            Array.Reverse( addrBytes );

            return new IPAddress( addrBytes );
        }
        public static uint GetIPAddress( IPAddress ipAddr )
        {
            byte[] addrBytes = ipAddr.GetAddressBytes();
            Array.Reverse( addrBytes );

            return BitConverter.ToUInt32( addrBytes, 0 );
        }


        public static uint EndianSwap( uint input )
        {
            return ( uint )IPAddress.NetworkToHostOrder( ( int )input );
        }
        public static ulong EndianSwap( ulong input )
        {
            return ( ulong )IPAddress.NetworkToHostOrder( ( long )input );
        }
        public static ushort EndianSwap( ushort input )
        {
            return ( ushort )IPAddress.NetworkToHostOrder( ( short )input );
        }
    }
}
