// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: visualizer_turn.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MM26.IO {

  /// <summary>Holder for reflection information generated from visualizer_turn.proto</summary>
  public static partial class VisualizerTurnReflection {

    #region Descriptor
    /// <summary>File descriptor for visualizer_turn.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static VisualizerTurnReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChV2aXN1YWxpemVyX3R1cm4ucHJvdG8SCnZpc3VhbGl6ZXIiJAoOVmlzdWFs",
            "aXplclR1cm4SEgoKdHVybk51bWJlchgBIAEoA0JTCjFtZWNoLm1hbmlhLk1N",
            "MjZHYW1lRW5naW5lLnZpc3VhbGl6ZXJDb21tdW5pY2F0aW9uQhRWaXN1YWxp",
            "emVyVHVyblByb3Rvc6oCB01NMjYuSU9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::MM26.IO.VisualizerTurn), global::MM26.IO.VisualizerTurn.Parser, new[]{ "TurnNumber" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class VisualizerTurn : pb::IMessage<VisualizerTurn> {
    private static readonly pb::MessageParser<VisualizerTurn> _parser = new pb::MessageParser<VisualizerTurn>(() => new VisualizerTurn());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<VisualizerTurn> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MM26.IO.VisualizerTurnReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VisualizerTurn() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VisualizerTurn(VisualizerTurn other) : this() {
      turnNumber_ = other.turnNumber_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VisualizerTurn Clone() {
      return new VisualizerTurn(this);
    }

    /// <summary>Field number for the "turnNumber" field.</summary>
    public const int TurnNumberFieldNumber = 1;
    private long turnNumber_;
    /// <summary>
    ///TODO: Insert data needed here
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long TurnNumber {
      get { return turnNumber_; }
      set {
        turnNumber_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as VisualizerTurn);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(VisualizerTurn other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TurnNumber != other.TurnNumber) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TurnNumber != 0L) hash ^= TurnNumber.GetHashCode();
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
      if (TurnNumber != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(TurnNumber);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (TurnNumber != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(TurnNumber);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(VisualizerTurn other) {
      if (other == null) {
        return;
      }
      if (other.TurnNumber != 0L) {
        TurnNumber = other.TurnNumber;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            TurnNumber = input.ReadInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
