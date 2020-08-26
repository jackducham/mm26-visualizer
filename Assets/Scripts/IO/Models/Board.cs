// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: board.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MM26.IO.Models {

  /// <summary>Holder for reflection information generated from board.proto</summary>
  public static partial class BoardReflection {

    #region Descriptor
    /// <summary>File descriptor for board.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static BoardReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cgtib2FyZC5wcm90bxIFYm9hcmQaCml0ZW0ucHJvdG8aD2NoYXJhY3Rlci5w",
            "cm90byJnCgVCb2FyZBIMCgRyb3dzGAEgASgFEg8KB2NvbHVtbnMYAiABKAUS",
            "GQoEZ3JpZBgDIAMoCzILLmJvYXJkLlRpbGUSJAoHcG9ydGFscxgEIAMoCzIT",
            "LmNoYXJhY3Rlci5Qb3NpdGlvbiKHAQoEVGlsZRInCgl0aWxlX3R5cGUYASAB",
            "KA4yFC5ib2FyZC5UaWxlLlRpbGVUeXBlEhkKBWl0ZW1zGAIgAygLMgouaXRl",
            "bS5JdGVtIjsKCFRpbGVUeXBlEggKBFZPSUQQABIJCgVCTEFOSxABEg4KCklN",
            "UEFTU0lCTEUQAhIKCgZQT1JUQUwQA0I+Ch5tZWNoLm1hbmlhLmVuZ2luZS5k",
            "b21haW4ubW9kZWxCC0JvYXJkUHJvdG9zqgIOTU0yNi5JTy5Nb2RlbHNiBnBy",
            "b3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::MM26.IO.Models.ItemReflection.Descriptor, global::MM26.IO.Models.CharacterReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::MM26.IO.Models.Board), global::MM26.IO.Models.Board.Parser, new[]{ "Rows", "Columns", "Grid", "Portals" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::MM26.IO.Models.Tile), global::MM26.IO.Models.Tile.Parser, new[]{ "TileType", "Items" }, null, new[]{ typeof(global::MM26.IO.Models.Tile.Types.TileType) }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Board : pb::IMessage<Board>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Board> _parser = new pb::MessageParser<Board>(() => new Board());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Board> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MM26.IO.Models.BoardReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Board() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Board(Board other) : this() {
      rows_ = other.rows_;
      columns_ = other.columns_;
      grid_ = other.grid_.Clone();
      portals_ = other.portals_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Board Clone() {
      return new Board(this);
    }

    /// <summary>Field number for the "rows" field.</summary>
    public const int RowsFieldNumber = 1;
    private int rows_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Rows {
      get { return rows_; }
      set {
        rows_ = value;
      }
    }

    /// <summary>Field number for the "columns" field.</summary>
    public const int ColumnsFieldNumber = 2;
    private int columns_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Columns {
      get { return columns_; }
      set {
        columns_ = value;
      }
    }

    /// <summary>Field number for the "grid" field.</summary>
    public const int GridFieldNumber = 3;
    private static readonly pb::FieldCodec<global::MM26.IO.Models.Tile> _repeated_grid_codec
        = pb::FieldCodec.ForMessage(26, global::MM26.IO.Models.Tile.Parser);
    private readonly pbc::RepeatedField<global::MM26.IO.Models.Tile> grid_ = new pbc::RepeatedField<global::MM26.IO.Models.Tile>();
    /// <summary>
    /// Protos only have 1D lists (row-major)
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::MM26.IO.Models.Tile> Grid {
      get { return grid_; }
    }

    /// <summary>Field number for the "portals" field.</summary>
    public const int PortalsFieldNumber = 4;
    private static readonly pb::FieldCodec<global::MM26.IO.Models.Position> _repeated_portals_codec
        = pb::FieldCodec.ForMessage(34, global::MM26.IO.Models.Position.Parser);
    private readonly pbc::RepeatedField<global::MM26.IO.Models.Position> portals_ = new pbc::RepeatedField<global::MM26.IO.Models.Position>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::MM26.IO.Models.Position> Portals {
      get { return portals_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Board);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Board other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Rows != other.Rows) return false;
      if (Columns != other.Columns) return false;
      if(!grid_.Equals(other.grid_)) return false;
      if(!portals_.Equals(other.portals_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Rows != 0) hash ^= Rows.GetHashCode();
      if (Columns != 0) hash ^= Columns.GetHashCode();
      hash ^= grid_.GetHashCode();
      hash ^= portals_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Rows != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Rows);
      }
      if (Columns != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Columns);
      }
      grid_.WriteTo(output, _repeated_grid_codec);
      portals_.WriteTo(output, _repeated_portals_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Rows != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Rows);
      }
      if (Columns != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Columns);
      }
      size += grid_.CalculateSize(_repeated_grid_codec);
      size += portals_.CalculateSize(_repeated_portals_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Board other) {
      if (other == null) {
        return;
      }
      if (other.Rows != 0) {
        Rows = other.Rows;
      }
      if (other.Columns != 0) {
        Columns = other.Columns;
      }
      grid_.Add(other.grid_);
      portals_.Add(other.portals_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Rows = input.ReadInt32();
            break;
          }
          case 16: {
            Columns = input.ReadInt32();
            break;
          }
          case 26: {
            grid_.AddEntriesFrom(input, _repeated_grid_codec);
            break;
          }
          case 34: {
            portals_.AddEntriesFrom(input, _repeated_portals_codec);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Rows = input.ReadInt32();
            break;
          }
          case 16: {
            Columns = input.ReadInt32();
            break;
          }
          case 26: {
            grid_.AddEntriesFrom(ref input, _repeated_grid_codec);
            break;
          }
          case 34: {
            portals_.AddEntriesFrom(ref input, _repeated_portals_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Tile : pb::IMessage<Tile>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Tile> _parser = new pb::MessageParser<Tile>(() => new Tile());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Tile> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MM26.IO.Models.BoardReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Tile() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Tile(Tile other) : this() {
      tileType_ = other.tileType_;
      items_ = other.items_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Tile Clone() {
      return new Tile(this);
    }

    /// <summary>Field number for the "tile_type" field.</summary>
    public const int TileTypeFieldNumber = 1;
    private global::MM26.IO.Models.Tile.Types.TileType tileType_ = global::MM26.IO.Models.Tile.Types.TileType.Void;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::MM26.IO.Models.Tile.Types.TileType TileType {
      get { return tileType_; }
      set {
        tileType_ = value;
      }
    }

    /// <summary>Field number for the "items" field.</summary>
    public const int ItemsFieldNumber = 2;
    private static readonly pb::FieldCodec<global::MM26.IO.Models.Item> _repeated_items_codec
        = pb::FieldCodec.ForMessage(18, global::MM26.IO.Models.Item.Parser);
    private readonly pbc::RepeatedField<global::MM26.IO.Models.Item> items_ = new pbc::RepeatedField<global::MM26.IO.Models.Item>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::MM26.IO.Models.Item> Items {
      get { return items_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Tile);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Tile other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TileType != other.TileType) return false;
      if(!items_.Equals(other.items_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TileType != global::MM26.IO.Models.Tile.Types.TileType.Void) hash ^= TileType.GetHashCode();
      hash ^= items_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (TileType != global::MM26.IO.Models.Tile.Types.TileType.Void) {
        output.WriteRawTag(8);
        output.WriteEnum((int) TileType);
      }
      items_.WriteTo(output, _repeated_items_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (TileType != global::MM26.IO.Models.Tile.Types.TileType.Void) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) TileType);
      }
      size += items_.CalculateSize(_repeated_items_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Tile other) {
      if (other == null) {
        return;
      }
      if (other.TileType != global::MM26.IO.Models.Tile.Types.TileType.Void) {
        TileType = other.TileType;
      }
      items_.Add(other.items_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            TileType = (global::MM26.IO.Models.Tile.Types.TileType) input.ReadEnum();
            break;
          }
          case 18: {
            items_.AddEntriesFrom(input, _repeated_items_codec);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            TileType = (global::MM26.IO.Models.Tile.Types.TileType) input.ReadEnum();
            break;
          }
          case 18: {
            items_.AddEntriesFrom(ref input, _repeated_items_codec);
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the Tile message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum TileType {
        [pbr::OriginalName("VOID")] Void = 0,
        [pbr::OriginalName("BLANK")] Blank = 1,
        [pbr::OriginalName("IMPASSIBLE")] Impassible = 2,
        [pbr::OriginalName("PORTAL")] Portal = 3,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
