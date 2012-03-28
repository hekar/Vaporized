//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 1591

// Generated from: content_manifest.proto
namespace SteamKit2.Internal
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ContentManifestPayload")]
  public partial class ContentManifestPayload : global::ProtoBuf.IExtensible
  {
    public ContentManifestPayload() {}
    
    private readonly global::System.Collections.Generic.List<ContentManifestPayload.FileMapping> _mappings = new global::System.Collections.Generic.List<ContentManifestPayload.FileMapping>();
    [global::ProtoBuf.ProtoMember(1, Name=@"mappings", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ContentManifestPayload.FileMapping> mappings
    {
      get { return _mappings; }
    }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FileMapping")]
  public partial class FileMapping : global::ProtoBuf.IExtensible
  {
    public FileMapping() {}
    

    private string _filename = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"filename", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::ProtoBuf.ProtoDefaultValue("")]
    public string filename
    {
      get { return _filename; }
      set { _filename = value; }
    }

    private ulong _size = default(ulong);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"size", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(ulong))]
    public ulong size
    {
      get { return _size; }
      set { _size = value; }
    }

    private uint _flags = default(uint);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"flags", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(uint))]
    public uint flags
    {
      get { return _flags; }
      set { _flags = value; }
    }

    private byte[] _sha_filename = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"sha_filename", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::ProtoBuf.ProtoDefaultValue(null)]
    public byte[] sha_filename
    {
      get { return _sha_filename; }
      set { _sha_filename = value; }
    }

    private byte[] _sha_content = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"sha_content", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::ProtoBuf.ProtoDefaultValue(null)]
    public byte[] sha_content
    {
      get { return _sha_content; }
      set { _sha_content = value; }
    }
    private readonly global::System.Collections.Generic.List<ContentManifestPayload.FileMapping.ChunkData> _chunks = new global::System.Collections.Generic.List<ContentManifestPayload.FileMapping.ChunkData>();
    [global::ProtoBuf.ProtoMember(6, Name=@"chunks", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ContentManifestPayload.FileMapping.ChunkData> chunks
    {
      get { return _chunks; }
    }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ChunkData")]
  public partial class ChunkData : global::ProtoBuf.IExtensible
  {
    public ChunkData() {}
    

    private byte[] _sha = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sha", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::ProtoBuf.ProtoDefaultValue(null)]
    public byte[] sha
    {
      get { return _sha; }
      set { _sha = value; }
    }

    private uint _crc = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"crc", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::ProtoBuf.ProtoDefaultValue(default(uint))]
    public uint crc
    {
      get { return _crc; }
      set { _crc = value; }
    }

    private ulong _offset = default(ulong);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"offset", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(ulong))]
    public ulong offset
    {
      get { return _offset; }
      set { _offset = value; }
    }

    private uint _cb_original = default(uint);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"cb_original", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(uint))]
    public uint cb_original
    {
      get { return _cb_original; }
      set { _cb_original = value; }
    }

    private uint _cb_compressed = default(uint);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"cb_compressed", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(uint))]
    public uint cb_compressed
    {
      get { return _cb_compressed; }
      set { _cb_compressed = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ContentManifestMetadata")]
  public partial class ContentManifestMetadata : global::ProtoBuf.IExtensible
  {
    public ContentManifestMetadata() {}
    

    private uint _depot_id = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"depot_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(uint))]
    public uint depot_id
    {
      get { return _depot_id; }
      set { _depot_id = value; }
    }

    private ulong _gid_manifest = default(ulong);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"gid_manifest", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(ulong))]
    public ulong gid_manifest
    {
      get { return _gid_manifest; }
      set { _gid_manifest = value; }
    }

    private uint _creation_time = default(uint);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"creation_time", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(uint))]
    public uint creation_time
    {
      get { return _creation_time; }
      set { _creation_time = value; }
    }

    private bool _filenames_encrypted = default(bool);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"filenames_encrypted", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::ProtoBuf.ProtoDefaultValue(default(bool))]
    public bool filenames_encrypted
    {
      get { return _filenames_encrypted; }
      set { _filenames_encrypted = value; }
    }

    private ulong _cb_disk_original = default(ulong);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"cb_disk_original", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(ulong))]
    public ulong cb_disk_original
    {
      get { return _cb_disk_original; }
      set { _cb_disk_original = value; }
    }

    private ulong _cb_disk_compressed = default(ulong);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"cb_disk_compressed", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(ulong))]
    public ulong cb_disk_compressed
    {
      get { return _cb_disk_compressed; }
      set { _cb_disk_compressed = value; }
    }

    private uint _unique_chunks = default(uint);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"unique_chunks", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(uint))]
    public uint unique_chunks
    {
      get { return _unique_chunks; }
      set { _unique_chunks = value; }
    }

    private uint _crc_encrypted = default(uint);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"crc_encrypted", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(uint))]
    public uint crc_encrypted
    {
      get { return _crc_encrypted; }
      set { _crc_encrypted = value; }
    }

    private uint _crc_clear = default(uint);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"crc_clear", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::ProtoBuf.ProtoDefaultValue(default(uint))]
    public uint crc_clear
    {
      get { return _crc_clear; }
      set { _crc_clear = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ContentManifestSignature")]
  public partial class ContentManifestSignature : global::ProtoBuf.IExtensible
  {
    public ContentManifestSignature() {}
    

    private byte[] _signature = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"signature", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::ProtoBuf.ProtoDefaultValue(null)]
    public byte[] signature
    {
      get { return _signature; }
      set { _signature = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}
#pragma warning restore 1591
