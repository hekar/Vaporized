#pragma warning disable 1591
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SteamKit2.Internal
{
	public interface ISteamSerializable
	{
		void Serialize(Stream stream);
		void Deserialize( Stream stream );
	}
	public interface ISteamSerializableHeader : ISteamSerializable
	{
		void SetEMsg( EMsg msg );
	}
	public interface ISteamSerializableMessage : ISteamSerializable
	{
		EMsg GetEMsg();
	}
	public interface IGCSerializableHeader : ISteamSerializable
	{
		void SetEMsg( EGCMsg msg );
	}
	public interface IGCSerializableMessage : ISteamSerializable
	{
		EGCMsg GetEMsg();
	}

	public class UdpHeader : ISteamSerializable
	{
		public static readonly uint MAGIC = 0x31305356;
		// Static size: 4
		public uint Magic { get; set; }
		// Static size: 2
		public ushort PayloadSize { get; set; }
		// Static size: 1
		public EUdpPacketType PacketType { get; set; }
		// Static size: 1
		public byte Flags { get; set; }
		// Static size: 4
		public uint SourceConnID { get; set; }
		// Static size: 4
		public uint DestConnID { get; set; }
		// Static size: 4
		public uint SeqThis { get; set; }
		// Static size: 4
		public uint SeqAck { get; set; }
		// Static size: 4
		public uint PacketsInMsg { get; set; }
		// Static size: 4
		public uint MsgStartSeq { get; set; }
		// Static size: 4
		public uint MsgSize { get; set; }

		public UdpHeader()
		{
			Magic = UdpHeader.MAGIC;
			PayloadSize = 0;
			PacketType = EUdpPacketType.Invalid;
			Flags = 0;
			SourceConnID = 512;
			DestConnID = 0;
			SeqThis = 0;
			SeqAck = 0;
			PacketsInMsg = 0;
			MsgStartSeq = 0;
			MsgSize = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( Magic );
			bw.Write( PayloadSize );
			bw.Write( (byte)PacketType );
			bw.Write( Flags );
			bw.Write( SourceConnID );
			bw.Write( DestConnID );
			bw.Write( SeqThis );
			bw.Write( SeqAck );
			bw.Write( PacketsInMsg );
			bw.Write( MsgStartSeq );
			bw.Write( MsgSize );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Magic = br.ReadUInt32();
			PayloadSize = br.ReadUInt16();
			PacketType = (EUdpPacketType)br.ReadByte();
			Flags = br.ReadByte();
			SourceConnID = br.ReadUInt32();
			DestConnID = br.ReadUInt32();
			SeqThis = br.ReadUInt32();
			SeqAck = br.ReadUInt32();
			PacketsInMsg = br.ReadUInt32();
			MsgStartSeq = br.ReadUInt32();
			MsgSize = br.ReadUInt32();
		}
	}

	public class ChallengeData : ISteamSerializable
	{
		public static readonly uint CHALLENGE_MASK = 0xA426DF2B;
		// Static size: 4
		public uint ChallengeValue { get; set; }
		// Static size: 4
		public uint ServerLoad { get; set; }

		public ChallengeData()
		{
			ChallengeValue = 0;
			ServerLoad = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( ChallengeValue );
			bw.Write( ServerLoad );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			ChallengeValue = br.ReadUInt32();
			ServerLoad = br.ReadUInt32();
		}
	}

	public class ConnectData : ISteamSerializable
	{
		public static readonly uint CHALLENGE_MASK = ChallengeData.CHALLENGE_MASK;
		// Static size: 4
		public uint ChallengeValue { get; set; }

		public ConnectData()
		{
			ChallengeValue = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( ChallengeValue );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			ChallengeValue = br.ReadUInt32();
		}
	}

	public class Accept : ISteamSerializable
	{

		public Accept()
		{
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );


		}

		public void Deserialize( Stream stream )
		{
		}
	}

	public class Datagram : ISteamSerializable
	{

		public Datagram()
		{
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );


		}

		public void Deserialize( Stream stream )
		{
		}
	}

	public class Disconnect : ISteamSerializable
	{

		public Disconnect()
		{
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );


		}

		public void Deserialize( Stream stream )
		{
		}
	}

	[StructLayout( LayoutKind.Sequential )]
	public class MsgHdr : ISteamSerializableHeader
	{
		public void SetEMsg( EMsg msg ) { this.Msg = msg; }

		// Static size: 4
		public EMsg Msg { get; set; }
		// Static size: 8
		public ulong TargetJobID { get; set; }
		// Static size: 8
		public ulong SourceJobID { get; set; }

		public MsgHdr()
		{
			Msg = EMsg.Invalid;
			TargetJobID = ulong.MaxValue;
			SourceJobID = ulong.MaxValue;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Msg );
			bw.Write( TargetJobID );
			bw.Write( SourceJobID );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Msg = (EMsg)br.ReadInt32();
			TargetJobID = br.ReadUInt64();
			SourceJobID = br.ReadUInt64();
		}
	}

	[StructLayout( LayoutKind.Sequential )]
	public class ExtendedClientMsgHdr : ISteamSerializableHeader
	{
		public void SetEMsg( EMsg msg ) { this.Msg = msg; }

		// Static size: 4
		public EMsg Msg { get; set; }
		// Static size: 1
		public byte HeaderSize { get; set; }
		// Static size: 2
		public ushort HeaderVersion { get; set; }
		// Static size: 8
		public ulong TargetJobID { get; set; }
		// Static size: 8
		public ulong SourceJobID { get; set; }
		// Static size: 1
		public byte HeaderCanary { get; set; }
		// Static size: 8
		private ulong steamID;
		public SteamID SteamID { get { return new SteamID( steamID ); } set { steamID = value.ConvertToUInt64(); } }
		// Static size: 4
		public int SessionID { get; set; }

		public ExtendedClientMsgHdr()
		{
			Msg = EMsg.Invalid;
			HeaderSize = 36;
			HeaderVersion = 2;
			TargetJobID = ulong.MaxValue;
			SourceJobID = ulong.MaxValue;
			HeaderCanary = 239;
			steamID = 0;
			SessionID = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Msg );
			bw.Write( HeaderSize );
			bw.Write( HeaderVersion );
			bw.Write( TargetJobID );
			bw.Write( SourceJobID );
			bw.Write( HeaderCanary );
			bw.Write( steamID );
			bw.Write( SessionID );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Msg = (EMsg)br.ReadInt32();
			HeaderSize = br.ReadByte();
			HeaderVersion = br.ReadUInt16();
			TargetJobID = br.ReadUInt64();
			SourceJobID = br.ReadUInt64();
			HeaderCanary = br.ReadByte();
			steamID = br.ReadUInt64();
			SessionID = br.ReadInt32();
		}
	}

	[StructLayout( LayoutKind.Sequential )]
	public class MsgHdrProtoBuf : ISteamSerializableHeader
	{
		public void SetEMsg( EMsg msg ) { this.Msg = msg; }

		// Static size: 4
		public EMsg Msg { get; set; }
		// Static size: 4
		public int HeaderLength { get; set; }
		// Static size: 0
		public SteamKit2.Internal.CMsgProtoBufHeader Proto { get; set; }

		public MsgHdrProtoBuf()
		{
			Msg = EMsg.Invalid;
			HeaderLength = 0;
			Proto = new SteamKit2.Internal.CMsgProtoBufHeader();
		}

		public void Serialize(Stream stream)
		{
			MemoryStream msProto = new MemoryStream();
			ProtoBuf.Serializer.Serialize<SteamKit2.Internal.CMsgProtoBufHeader>(msProto, Proto);
			HeaderLength = (int)msProto.Length;
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)MsgUtil.MakeMsg( Msg, true ) );
			bw.Write( HeaderLength );
			bw.Write( msProto.ToArray() );

			msProto.Close();
		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Msg = (EMsg)MsgUtil.GetMsg( (uint)br.ReadInt32() );
			HeaderLength = br.ReadInt32();
			using( MemoryStream msProto = new MemoryStream( br.ReadBytes( HeaderLength ) ) )
				Proto = ProtoBuf.Serializer.Deserialize<SteamKit2.Internal.CMsgProtoBufHeader>( msProto );
		}
	}

	[StructLayout( LayoutKind.Sequential )]
	public class MsgGCHdrProtoBuf : IGCSerializableHeader
	{
		public void SetEMsg( EGCMsg msg ) { this.Msg = msg; }

		// Static size: 4
		public EGCMsg Msg { get; set; }
		// Static size: 4
		public int HeaderLength { get; set; }
		// Static size: 0
		public SteamKit2.Internal.GC.CMsgProtoBufHeader ProtoHeader { get; set; }

		public MsgGCHdrProtoBuf()
		{
			Msg = EGCMsg.Invalid;
			HeaderLength = 0;
			ProtoHeader = new SteamKit2.Internal.GC.CMsgProtoBufHeader();
		}

		public void Serialize(Stream stream)
		{
			MemoryStream msProtoHeader = new MemoryStream();
			ProtoBuf.Serializer.Serialize<SteamKit2.Internal.GC.CMsgProtoBufHeader>(msProtoHeader, ProtoHeader);
			HeaderLength = (int)msProtoHeader.Length;
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)MsgUtil.MakeGCMsg( Msg, true ) );
			bw.Write( HeaderLength );
			bw.Write( msProtoHeader.ToArray() );

			msProtoHeader.Close();
		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Msg = (EGCMsg)MsgUtil.GetGCMsg( (uint)br.ReadInt32() );
			HeaderLength = br.ReadInt32();
			using( MemoryStream msProtoHeader = new MemoryStream( br.ReadBytes( HeaderLength ) ) )
				ProtoHeader = ProtoBuf.Serializer.Deserialize<SteamKit2.Internal.GC.CMsgProtoBufHeader>( msProtoHeader );
		}
	}

	[StructLayout( LayoutKind.Sequential )]
	public class MsgGCHdr : IGCSerializableHeader
	{
		public void SetEMsg( EGCMsg msg ) { }

		// Static size: 2
		public ushort HeaderVersion { get; set; }
		// Static size: 8
		public long TargetJobID { get; set; }
		// Static size: 8
		public long SourceJobID { get; set; }

		public MsgGCHdr()
		{
			HeaderVersion = 1;
			TargetJobID = -1;
			SourceJobID = -1;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( HeaderVersion );
			bw.Write( TargetJobID );
			bw.Write( SourceJobID );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			HeaderVersion = br.ReadUInt16();
			TargetJobID = br.ReadInt64();
			SourceJobID = br.ReadInt64();
		}
	}

	public class MsgClientJustStrings : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.Invalid; }


		public MsgClientJustStrings()
		{
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );


		}

		public void Deserialize( Stream stream )
		{
		}
	}

	public class MsgClientGenericResponse : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.Invalid; }

		// Static size: 4
		public EResult Result { get; set; }

		public MsgClientGenericResponse()
		{
			Result = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Result );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Result = (EResult)br.ReadInt32();
		}
	}

	public class MsgChannelEncryptRequest : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ChannelEncryptRequest; }

		public static readonly uint PROTOCOL_VERSION = 1;
		// Static size: 4
		public uint ProtocolVersion { get; set; }
		// Static size: 4
		public EUniverse Universe { get; set; }

		public MsgChannelEncryptRequest()
		{
			ProtocolVersion = MsgChannelEncryptRequest.PROTOCOL_VERSION;
			Universe = EUniverse.Invalid;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( ProtocolVersion );
			bw.Write( (int)Universe );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			ProtocolVersion = br.ReadUInt32();
			Universe = (EUniverse)br.ReadInt32();
		}
	}

	public class MsgChannelEncryptResponse : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ChannelEncryptResponse; }

		// Static size: 4
		public uint ProtocolVersion { get; set; }
		// Static size: 4
		public uint KeySize { get; set; }

		public MsgChannelEncryptResponse()
		{
			ProtocolVersion = MsgChannelEncryptRequest.PROTOCOL_VERSION;
			KeySize = 128;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( ProtocolVersion );
			bw.Write( KeySize );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			ProtocolVersion = br.ReadUInt32();
			KeySize = br.ReadUInt32();
		}
	}

	public class MsgChannelEncryptResult : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ChannelEncryptResult; }

		// Static size: 4
		public EResult Result { get; set; }

		public MsgChannelEncryptResult()
		{
			Result = EResult.Invalid;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Result );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Result = (EResult)br.ReadInt32();
		}
	}

	public class MsgClientNewLoginKey : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientNewLoginKey; }

		// Static size: 4
		public uint UniqueID { get; set; }
		// Static size: 20
		public byte[] LoginKey { get; set; }

		public MsgClientNewLoginKey()
		{
			UniqueID = 0;
			LoginKey = new byte[20];
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( UniqueID );
			bw.Write( LoginKey );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			UniqueID = br.ReadUInt32();
			LoginKey = br.ReadBytes( 20 );
		}
	}

	public class MsgClientNewLoginKeyAccepted : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientNewLoginKeyAccepted; }

		// Static size: 4
		public uint UniqueID { get; set; }

		public MsgClientNewLoginKeyAccepted()
		{
			UniqueID = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( UniqueID );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			UniqueID = br.ReadUInt32();
		}
	}

	public class MsgClientLogon : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientLogon; }

		public static readonly uint ObfuscationMask = 0xBAADF00D;
		public static readonly uint CurrentProtocol = 65575;
		public static readonly uint ProtocolVerMajorMask = 0xFFFF0000;
		public static readonly uint ProtocolVerMinorMask = 0xFFFF;
		public static readonly ushort ProtocolVerMinorMinGameServers = 4;
		public static readonly ushort ProtocolVerMinorMinForSupportingEMsgMulti = 12;
		public static readonly ushort ProtocolVerMinorMinForSupportingEMsgClientEncryptPct = 14;
		public static readonly ushort ProtocolVerMinorMinForExtendedMsgHdr = 17;
		public static readonly ushort ProtocolVerMinorMinForCellId = 18;
		public static readonly ushort ProtocolVerMinorMinForSessionIDLast = 19;
		public static readonly ushort ProtocolVerMinorMinForServerAvailablityMsgs = 24;
		public static readonly ushort ProtocolVerMinorMinClients = 25;
		public static readonly ushort ProtocolVerMinorMinForOSType = 26;
		public static readonly ushort ProtocolVerMinorMinForCegApplyPESig = 27;
		public static readonly ushort ProtocolVerMinorMinForMarketingMessages2 = 27;
		public static readonly ushort ProtocolVerMinorMinForAnyProtoBufMessages = 28;
		public static readonly ushort ProtocolVerMinorMinForProtoBufLoggedOffMessage = 28;
		public static readonly ushort ProtocolVerMinorMinForProtoBufMultiMessages = 28;
		public static readonly ushort ProtocolVerMinorMinForSendingProtocolToUFS = 30;
		public static readonly ushort ProtocolVerMinorMinForMachineAuth = 33;

		public MsgClientLogon()
		{
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );


		}

		public void Deserialize( Stream stream )
		{
		}
	}

	public class MsgClientVACBanStatus : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientVACBanStatus; }

		// Static size: 4
		public uint NumBans { get; set; }

		public MsgClientVACBanStatus()
		{
			NumBans = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( NumBans );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			NumBans = br.ReadUInt32();
		}
	}

	public class MsgClientAppUsageEvent : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientAppUsageEvent; }

		// Static size: 4
		public EAppUsageEvent AppUsageEvent { get; set; }
		// Static size: 8
		private ulong gameID;
		public GameID GameID { get { return new GameID( gameID ); } set { gameID = value.ToUInt64(); } }
		// Static size: 2
		public ushort Offline { get; set; }

		public MsgClientAppUsageEvent()
		{
			AppUsageEvent = 0;
			gameID = 0;
			Offline = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)AppUsageEvent );
			bw.Write( gameID );
			bw.Write( Offline );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			AppUsageEvent = (EAppUsageEvent)br.ReadInt32();
			gameID = br.ReadUInt64();
			Offline = br.ReadUInt16();
		}
	}

	public class MsgClientEmailAddrInfo : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientEmailAddrInfo; }

		// Static size: 4
		public uint PasswordStrength { get; set; }
		// Static size: 4
		public uint FlagsAccountSecurityPolicy { get; set; }
		// Static size: 1
		private byte validated;
		public bool Validated { get { return ( validated == 1 ); } set { validated = ( byte )( value ? 1 : 0 ); } }

		public MsgClientEmailAddrInfo()
		{
			PasswordStrength = 0;
			FlagsAccountSecurityPolicy = 0;
			validated = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( PasswordStrength );
			bw.Write( FlagsAccountSecurityPolicy );
			bw.Write( validated );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			PasswordStrength = br.ReadUInt32();
			FlagsAccountSecurityPolicy = br.ReadUInt32();
			validated = br.ReadByte();
		}
	}

	public class MsgClientUpdateGuestPassesList : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientUpdateGuestPassesList; }

		// Static size: 4
		public EResult Result { get; set; }
		// Static size: 4
		public int CountGuestPassesToGive { get; set; }
		// Static size: 4
		public int CountGuestPassesToRedeem { get; set; }

		public MsgClientUpdateGuestPassesList()
		{
			Result = 0;
			CountGuestPassesToGive = 0;
			CountGuestPassesToRedeem = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Result );
			bw.Write( CountGuestPassesToGive );
			bw.Write( CountGuestPassesToRedeem );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Result = (EResult)br.ReadInt32();
			CountGuestPassesToGive = br.ReadInt32();
			CountGuestPassesToRedeem = br.ReadInt32();
		}
	}

	public class MsgClientRequestedClientStats : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientRequestedClientStats; }

		// Static size: 4
		public int CountStats { get; set; }

		public MsgClientRequestedClientStats()
		{
			CountStats = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( CountStats );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			CountStats = br.ReadInt32();
		}
	}

	public class MsgClientP2PIntroducerMessage : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientP2PIntroducerMessage; }

		// Static size: 8
		private ulong steamID;
		public SteamID SteamID { get { return new SteamID( steamID ); } set { steamID = value.ConvertToUInt64(); } }
		// Static size: 4
		public EIntroducerRouting RoutingType { get; set; }
		// Static size: 1450
		public byte[] Data { get; set; }
		// Static size: 4
		public uint DataLen { get; set; }

		public MsgClientP2PIntroducerMessage()
		{
			steamID = 0;
			RoutingType = 0;
			Data = new byte[1450];
			DataLen = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamID );
			bw.Write( (int)RoutingType );
			bw.Write( Data );
			bw.Write( DataLen );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamID = br.ReadUInt64();
			RoutingType = (EIntroducerRouting)br.ReadInt32();
			Data = br.ReadBytes( 1450 );
			DataLen = br.ReadUInt32();
		}
	}

	public class MsgClientOGSBeginSession : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientOGSBeginSession; }

		// Static size: 1
		public byte AccountType { get; set; }
		// Static size: 8
		private ulong accountId;
		public SteamID AccountId { get { return new SteamID( accountId ); } set { accountId = value.ConvertToUInt64(); } }
		// Static size: 4
		public uint AppId { get; set; }
		// Static size: 4
		public uint TimeStarted { get; set; }

		public MsgClientOGSBeginSession()
		{
			AccountType = 0;
			accountId = 0;
			AppId = 0;
			TimeStarted = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( AccountType );
			bw.Write( accountId );
			bw.Write( AppId );
			bw.Write( TimeStarted );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			AccountType = br.ReadByte();
			accountId = br.ReadUInt64();
			AppId = br.ReadUInt32();
			TimeStarted = br.ReadUInt32();
		}
	}

	public class MsgClientOGSBeginSessionResponse : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientOGSBeginSessionResponse; }

		// Static size: 4
		public EResult Result { get; set; }
		// Static size: 1
		private byte collectingAny;
		public bool CollectingAny { get { return ( collectingAny == 1 ); } set { collectingAny = ( byte )( value ? 1 : 0 ); } }
		// Static size: 1
		private byte collectingDetails;
		public bool CollectingDetails { get { return ( collectingDetails == 1 ); } set { collectingDetails = ( byte )( value ? 1 : 0 ); } }
		// Static size: 8
		public ulong SessionId { get; set; }

		public MsgClientOGSBeginSessionResponse()
		{
			Result = 0;
			collectingAny = 0;
			collectingDetails = 0;
			SessionId = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Result );
			bw.Write( collectingAny );
			bw.Write( collectingDetails );
			bw.Write( SessionId );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Result = (EResult)br.ReadInt32();
			collectingAny = br.ReadByte();
			collectingDetails = br.ReadByte();
			SessionId = br.ReadUInt64();
		}
	}

	public class MsgClientOGSEndSession : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientOGSEndSession; }

		// Static size: 8
		public ulong SessionId { get; set; }
		// Static size: 4
		public uint TimeEnded { get; set; }
		// Static size: 4
		public int ReasonCode { get; set; }
		// Static size: 4
		public int CountAttributes { get; set; }

		public MsgClientOGSEndSession()
		{
			SessionId = 0;
			TimeEnded = 0;
			ReasonCode = 0;
			CountAttributes = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( SessionId );
			bw.Write( TimeEnded );
			bw.Write( ReasonCode );
			bw.Write( CountAttributes );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			SessionId = br.ReadUInt64();
			TimeEnded = br.ReadUInt32();
			ReasonCode = br.ReadInt32();
			CountAttributes = br.ReadInt32();
		}
	}

	public class MsgClientOGSEndSessionResponse : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientOGSEndSessionResponse; }

		// Static size: 4
		public EResult Result { get; set; }

		public MsgClientOGSEndSessionResponse()
		{
			Result = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Result );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Result = (EResult)br.ReadInt32();
		}
	}

	public class MsgClientOGSWriteRow : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientOGSWriteRow; }

		// Static size: 8
		public ulong SessionId { get; set; }
		// Static size: 4
		public int CountAttributes { get; set; }

		public MsgClientOGSWriteRow()
		{
			SessionId = 0;
			CountAttributes = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( SessionId );
			bw.Write( CountAttributes );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			SessionId = br.ReadUInt64();
			CountAttributes = br.ReadInt32();
		}
	}

	public class MsgClientGetFriendsWhoPlayGame : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientGetFriendsWhoPlayGame; }

		// Static size: 8
		private ulong gameId;
		public GameID GameId { get { return new GameID( gameId ); } set { gameId = value.ToUInt64(); } }

		public MsgClientGetFriendsWhoPlayGame()
		{
			gameId = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( gameId );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			gameId = br.ReadUInt64();
		}
	}

	public class MsgClientGetFriendsWhoPlayGameResponse : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientGetFriendsWhoPlayGameResponse; }

		// Static size: 4
		public EResult Result { get; set; }
		// Static size: 8
		private ulong gameId;
		public GameID GameId { get { return new GameID( gameId ); } set { gameId = value.ToUInt64(); } }
		// Static size: 4
		public uint CountFriends { get; set; }

		public MsgClientGetFriendsWhoPlayGameResponse()
		{
			Result = 0;
			gameId = 0;
			CountFriends = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Result );
			bw.Write( gameId );
			bw.Write( CountFriends );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Result = (EResult)br.ReadInt32();
			gameId = br.ReadUInt64();
			CountFriends = br.ReadUInt32();
		}
	}

	public class MsgGSPerformHardwareSurvey : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.GSPerformHardwareSurvey; }

		// Static size: 4
		public uint Flags { get; set; }

		public MsgGSPerformHardwareSurvey()
		{
			Flags = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( Flags );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Flags = br.ReadUInt32();
		}
	}

	public class MsgGSGetPlayStatsResponse : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.GSGetPlayStatsResponse; }

		// Static size: 4
		public EResult Result { get; set; }
		// Static size: 4
		public int Rank { get; set; }
		// Static size: 4
		public uint LifetimeConnects { get; set; }
		// Static size: 4
		public uint LifetimeMinutesPlayed { get; set; }

		public MsgGSGetPlayStatsResponse()
		{
			Result = 0;
			Rank = 0;
			LifetimeConnects = 0;
			LifetimeMinutesPlayed = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Result );
			bw.Write( Rank );
			bw.Write( LifetimeConnects );
			bw.Write( LifetimeMinutesPlayed );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Result = (EResult)br.ReadInt32();
			Rank = br.ReadInt32();
			LifetimeConnects = br.ReadUInt32();
			LifetimeMinutesPlayed = br.ReadUInt32();
		}
	}

	public class MsgGSGetReputationResponse : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.GSGetReputationResponse; }

		// Static size: 4
		public EResult Result { get; set; }
		// Static size: 4
		public uint ReputationScore { get; set; }
		// Static size: 1
		private byte banned;
		public bool Banned { get { return ( banned == 1 ); } set { banned = ( byte )( value ? 1 : 0 ); } }
		// Static size: 4
		public uint BannedIp { get; set; }
		// Static size: 2
		public ushort BannedPort { get; set; }
		// Static size: 8
		public ulong BannedGameId { get; set; }
		// Static size: 4
		public uint TimeBanExpires { get; set; }

		public MsgGSGetReputationResponse()
		{
			Result = 0;
			ReputationScore = 0;
			banned = 0;
			BannedIp = 0;
			BannedPort = 0;
			BannedGameId = 0;
			TimeBanExpires = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Result );
			bw.Write( ReputationScore );
			bw.Write( banned );
			bw.Write( BannedIp );
			bw.Write( BannedPort );
			bw.Write( BannedGameId );
			bw.Write( TimeBanExpires );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Result = (EResult)br.ReadInt32();
			ReputationScore = br.ReadUInt32();
			banned = br.ReadByte();
			BannedIp = br.ReadUInt32();
			BannedPort = br.ReadUInt16();
			BannedGameId = br.ReadUInt64();
			TimeBanExpires = br.ReadUInt32();
		}
	}

	public class MsgGSDeny : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.GSDeny; }

		// Static size: 8
		private ulong steamId;
		public SteamID SteamId { get { return new SteamID( steamId ); } set { steamId = value.ConvertToUInt64(); } }
		// Static size: 4
		public EDenyReason DenyReason { get; set; }

		public MsgGSDeny()
		{
			steamId = 0;
			DenyReason = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamId );
			bw.Write( (int)DenyReason );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamId = br.ReadUInt64();
			DenyReason = (EDenyReason)br.ReadInt32();
		}
	}

	public class MsgGSApprove : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.GSApprove; }

		// Static size: 8
		private ulong steamId;
		public SteamID SteamId { get { return new SteamID( steamId ); } set { steamId = value.ConvertToUInt64(); } }

		public MsgGSApprove()
		{
			steamId = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamId );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamId = br.ReadUInt64();
		}
	}

	public class MsgGSKick : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.GSKick; }

		// Static size: 8
		private ulong steamId;
		public SteamID SteamId { get { return new SteamID( steamId ); } set { steamId = value.ConvertToUInt64(); } }
		// Static size: 4
		public EDenyReason DenyReason { get; set; }
		// Static size: 4
		public int WaitTilMapChange { get; set; }

		public MsgGSKick()
		{
			steamId = 0;
			DenyReason = 0;
			WaitTilMapChange = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamId );
			bw.Write( (int)DenyReason );
			bw.Write( WaitTilMapChange );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamId = br.ReadUInt64();
			DenyReason = (EDenyReason)br.ReadInt32();
			WaitTilMapChange = br.ReadInt32();
		}
	}

	public class MsgGSGetUserGroupStatus : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.GSGetUserGroupStatus; }

		// Static size: 8
		private ulong steamIdUser;
		public SteamID SteamIdUser { get { return new SteamID( steamIdUser ); } set { steamIdUser = value.ConvertToUInt64(); } }
		// Static size: 8
		private ulong steamIdGroup;
		public SteamID SteamIdGroup { get { return new SteamID( steamIdGroup ); } set { steamIdGroup = value.ConvertToUInt64(); } }

		public MsgGSGetUserGroupStatus()
		{
			steamIdUser = 0;
			steamIdGroup = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamIdUser );
			bw.Write( steamIdGroup );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamIdUser = br.ReadUInt64();
			steamIdGroup = br.ReadUInt64();
		}
	}

	public class MsgGSGetUserGroupStatusResponse : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.GSGetUserGroupStatusResponse; }

		// Static size: 8
		private ulong steamIdUser;
		public SteamID SteamIdUser { get { return new SteamID( steamIdUser ); } set { steamIdUser = value.ConvertToUInt64(); } }
		// Static size: 8
		private ulong steamIdGroup;
		public SteamID SteamIdGroup { get { return new SteamID( steamIdGroup ); } set { steamIdGroup = value.ConvertToUInt64(); } }
		// Static size: 4
		public EClanRelationship ClanRelationship { get; set; }
		// Static size: 4
		public EClanRank ClanRank { get; set; }

		public MsgGSGetUserGroupStatusResponse()
		{
			steamIdUser = 0;
			steamIdGroup = 0;
			ClanRelationship = 0;
			ClanRank = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamIdUser );
			bw.Write( steamIdGroup );
			bw.Write( (int)ClanRelationship );
			bw.Write( (int)ClanRank );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamIdUser = br.ReadUInt64();
			steamIdGroup = br.ReadUInt64();
			ClanRelationship = (EClanRelationship)br.ReadInt32();
			ClanRank = (EClanRank)br.ReadInt32();
		}
	}

	public class MsgClientJoinChat : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientJoinChat; }

		// Static size: 8
		private ulong steamIdChat;
		public SteamID SteamIdChat { get { return new SteamID( steamIdChat ); } set { steamIdChat = value.ConvertToUInt64(); } }
		// Static size: 1
		private byte isVoiceSpeaker;
		public bool IsVoiceSpeaker { get { return ( isVoiceSpeaker == 1 ); } set { isVoiceSpeaker = ( byte )( value ? 1 : 0 ); } }

		public MsgClientJoinChat()
		{
			steamIdChat = 0;
			isVoiceSpeaker = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamIdChat );
			bw.Write( isVoiceSpeaker );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamIdChat = br.ReadUInt64();
			isVoiceSpeaker = br.ReadByte();
		}
	}

	public class MsgClientChatEnter : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientChatEnter; }

		// Static size: 8
		private ulong steamIdChat;
		public SteamID SteamIdChat { get { return new SteamID( steamIdChat ); } set { steamIdChat = value.ConvertToUInt64(); } }
		// Static size: 8
		private ulong steamIdFriend;
		public SteamID SteamIdFriend { get { return new SteamID( steamIdFriend ); } set { steamIdFriend = value.ConvertToUInt64(); } }
		// Static size: 4
		public EChatRoomType ChatRoomType { get; set; }
		// Static size: 8
		private ulong steamIdOwner;
		public SteamID SteamIdOwner { get { return new SteamID( steamIdOwner ); } set { steamIdOwner = value.ConvertToUInt64(); } }
		// Static size: 8
		private ulong steamIdClan;
		public SteamID SteamIdClan { get { return new SteamID( steamIdClan ); } set { steamIdClan = value.ConvertToUInt64(); } }
		// Static size: 1
		public byte ChatFlags { get; set; }
		// Static size: 4
		public EChatRoomEnterResponse EnterResponse { get; set; }

		public MsgClientChatEnter()
		{
			steamIdChat = 0;
			steamIdFriend = 0;
			ChatRoomType = 0;
			steamIdOwner = 0;
			steamIdClan = 0;
			ChatFlags = 0;
			EnterResponse = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamIdChat );
			bw.Write( steamIdFriend );
			bw.Write( (int)ChatRoomType );
			bw.Write( steamIdOwner );
			bw.Write( steamIdClan );
			bw.Write( ChatFlags );
			bw.Write( (int)EnterResponse );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamIdChat = br.ReadUInt64();
			steamIdFriend = br.ReadUInt64();
			ChatRoomType = (EChatRoomType)br.ReadInt32();
			steamIdOwner = br.ReadUInt64();
			steamIdClan = br.ReadUInt64();
			ChatFlags = br.ReadByte();
			EnterResponse = (EChatRoomEnterResponse)br.ReadInt32();
		}
	}

	public class MsgClientChatMsg : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientChatMsg; }

		// Static size: 8
		private ulong steamIdChatter;
		public SteamID SteamIdChatter { get { return new SteamID( steamIdChatter ); } set { steamIdChatter = value.ConvertToUInt64(); } }
		// Static size: 8
		private ulong steamIdChatRoom;
		public SteamID SteamIdChatRoom { get { return new SteamID( steamIdChatRoom ); } set { steamIdChatRoom = value.ConvertToUInt64(); } }
		// Static size: 4
		public EChatEntryType ChatMsgType { get; set; }

		public MsgClientChatMsg()
		{
			steamIdChatter = 0;
			steamIdChatRoom = 0;
			ChatMsgType = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamIdChatter );
			bw.Write( steamIdChatRoom );
			bw.Write( (int)ChatMsgType );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamIdChatter = br.ReadUInt64();
			steamIdChatRoom = br.ReadUInt64();
			ChatMsgType = (EChatEntryType)br.ReadInt32();
		}
	}

	public class MsgClientChatMemberInfo : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientChatMemberInfo; }

		// Static size: 8
		private ulong steamIdChat;
		public SteamID SteamIdChat { get { return new SteamID( steamIdChat ); } set { steamIdChat = value.ConvertToUInt64(); } }
		// Static size: 4
		public EChatInfoType Type { get; set; }

		public MsgClientChatMemberInfo()
		{
			steamIdChat = 0;
			Type = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamIdChat );
			bw.Write( (int)Type );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamIdChat = br.ReadUInt64();
			Type = (EChatInfoType)br.ReadInt32();
		}
	}

	public class MsgClientChatAction : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientChatAction; }

		// Static size: 8
		private ulong steamIdChat;
		public SteamID SteamIdChat { get { return new SteamID( steamIdChat ); } set { steamIdChat = value.ConvertToUInt64(); } }
		// Static size: 8
		private ulong steamIdUserToActOn;
		public SteamID SteamIdUserToActOn { get { return new SteamID( steamIdUserToActOn ); } set { steamIdUserToActOn = value.ConvertToUInt64(); } }
		// Static size: 4
		public EChatAction ChatAction { get; set; }

		public MsgClientChatAction()
		{
			steamIdChat = 0;
			steamIdUserToActOn = 0;
			ChatAction = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamIdChat );
			bw.Write( steamIdUserToActOn );
			bw.Write( (int)ChatAction );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamIdChat = br.ReadUInt64();
			steamIdUserToActOn = br.ReadUInt64();
			ChatAction = (EChatAction)br.ReadInt32();
		}
	}

	public class MsgClientChatActionResult : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientChatActionResult; }

		// Static size: 8
		private ulong steamIdChat;
		public SteamID SteamIdChat { get { return new SteamID( steamIdChat ); } set { steamIdChat = value.ConvertToUInt64(); } }
		// Static size: 8
		private ulong steamIdUserActedOn;
		public SteamID SteamIdUserActedOn { get { return new SteamID( steamIdUserActedOn ); } set { steamIdUserActedOn = value.ConvertToUInt64(); } }
		// Static size: 4
		public EChatAction ChatAction { get; set; }
		// Static size: 4
		public EChatActionResult ActionResult { get; set; }

		public MsgClientChatActionResult()
		{
			steamIdChat = 0;
			steamIdUserActedOn = 0;
			ChatAction = 0;
			ActionResult = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( steamIdChat );
			bw.Write( steamIdUserActedOn );
			bw.Write( (int)ChatAction );
			bw.Write( (int)ActionResult );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			steamIdChat = br.ReadUInt64();
			steamIdUserActedOn = br.ReadUInt64();
			ChatAction = (EChatAction)br.ReadInt32();
			ActionResult = (EChatActionResult)br.ReadInt32();
		}
	}

	public class MsgClientGetNumberOfCurrentPlayers : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientGetNumberOfCurrentPlayers; }

		// Static size: 8
		private ulong gameID;
		public GameID GameID { get { return new GameID( gameID ); } set { gameID = value.ToUInt64(); } }

		public MsgClientGetNumberOfCurrentPlayers()
		{
			gameID = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( gameID );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			gameID = br.ReadUInt64();
		}
	}

	public class MsgClientGetNumberOfCurrentPlayersResponse : ISteamSerializableMessage
	{
		public EMsg GetEMsg() { return EMsg.ClientGetNumberOfCurrentPlayersResponse; }

		// Static size: 4
		public EResult Result { get; set; }
		// Static size: 4
		public uint NumPlayers { get; set; }

		public MsgClientGetNumberOfCurrentPlayersResponse()
		{
			Result = 0;
			NumPlayers = 0;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter( stream );

			bw.Write( (int)Result );
			bw.Write( NumPlayers );

		}

		public void Deserialize( Stream stream )
		{
			BinaryReader br = new BinaryReader( stream );

			Result = (EResult)br.ReadInt32();
			NumPlayers = br.ReadUInt32();
		}
	}

}
#pragma warning restore 1591
