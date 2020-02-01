// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: visualizer_change.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MM26.IO {

  /// <summary>Holder for reflection information generated from visualizer_change.proto</summary>
  public static partial class VisualizerChangeReflection {

    #region Descriptor
    /// <summary>File descriptor for visualizer_change.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static VisualizerChangeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Chd2aXN1YWxpemVyX2NoYW5nZS5wcm90bxIKdmlzdWFsaXplciIoChBWaXN1",
            "YWxpemVyQ2hhbmdlEhQKDGNoYW5nZU51bWJlchgBIAEoA0JTCjFtZWNoLm1h",
            "bmlhLk1NMjZHYW1lRW5naW5lLnZpc3VhbGl6ZXJDb21tdW5pY2F0aW9uQhRW",
            "aXN1YWxpemVyVHVyblByb3Rvc6oCB01NMjYuSU9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::MM26.IO.VisualizerChange), global::MM26.IO.VisualizerChange.Parser, new[]{ "ChangeNumber" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class VisualizerChange : pb::IMessage<VisualizerChange> {
    private static readonly pb::MessageParser<VisualizerChange> _parser = new pb::MessageParser<VisualizerChange>(() => new VisualizerChange());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<VisualizerChange> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MM26.IO.VisualizerChangeReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VisualizerChange() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VisualizerChange(VisualizerChange other) : this() {
      changeNumber_ = other.changeNumber_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VisualizerChange Clone() {
      return new VisualizerChange(this);
    }

    /// <summary>Field number for the "changeNumber" field.</summary>
    public const int ChangeNumberFieldNumber = 1;
    private long changeNumber_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long ChangeNumber {
      get { return changeNumber_; }
      set {
        changeNumber_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as VisualizerChange);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(VisualizerChange other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ChangeNumber != other.ChangeNumber) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ChangeNumber != 0L) hash ^= ChangeNumber.GetHashCode();
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
      if (ChangeNumber != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(ChangeNumber);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ChangeNumber != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(ChangeNumber);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(VisualizerChange other) {
      if (other == null) {
        return;
      }
      if (other.ChangeNumber != 0L) {
        ChangeNumber = other.ChangeNumber;
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
            ChangeNumber = input.ReadInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
