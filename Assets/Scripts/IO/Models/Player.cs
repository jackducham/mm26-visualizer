// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: player.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MM26.IO.Models {

  /// <summary>Holder for reflection information generated from player.proto</summary>
  public static partial class PlayerReflection {

    #region Descriptor
    /// <summary>File descriptor for player.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PlayerReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxwbGF5ZXIucHJvdG8SFHBsYXllcl9jb21tdW5pY2F0aW9uGgpnYW1lLnBy",
            "b3RvIkYKClBsYXllclR1cm4SIwoKZ2FtZV9zdGF0ZRgBIAEoCzIPLmdhbWUu",
            "R2FtZVN0YXRlEhMKC3BsYXllcl9uYW1lGAIgASgJQj8KHm1lY2gubWFuaWEu",
            "ZW5naW5lLmRvbWFpbi5tb2RlbEIMUGxheWVyUHJvdG9zqgIOTU0yNi5JTy5N",
            "b2RlbHNiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::MM26.IO.Models.GameReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::MM26.IO.Models.PlayerTurn), global::MM26.IO.Models.PlayerTurn.Parser, new[]{ "GameState", "PlayerName" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Proto sent from engine to player
  /// </summary>
  public sealed partial class PlayerTurn : pb::IMessage<PlayerTurn>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<PlayerTurn> _parser = new pb::MessageParser<PlayerTurn>(() => new PlayerTurn());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PlayerTurn> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MM26.IO.Models.PlayerReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlayerTurn() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlayerTurn(PlayerTurn other) : this() {
      gameState_ = other.gameState_ != null ? other.gameState_.Clone() : null;
      playerName_ = other.playerName_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlayerTurn Clone() {
      return new PlayerTurn(this);
    }

    /// <summary>Field number for the "game_state" field.</summary>
    public const int GameStateFieldNumber = 1;
    private global::MM26.IO.Models.GameState gameState_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::MM26.IO.Models.GameState GameState {
      get { return gameState_; }
      set {
        gameState_ = value;
      }
    }

    /// <summary>Field number for the "player_name" field.</summary>
    public const int PlayerNameFieldNumber = 2;
    private string playerName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string PlayerName {
      get { return playerName_; }
      set {
        playerName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PlayerTurn);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PlayerTurn other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(GameState, other.GameState)) return false;
      if (PlayerName != other.PlayerName) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (gameState_ != null) hash ^= GameState.GetHashCode();
      if (PlayerName.Length != 0) hash ^= PlayerName.GetHashCode();
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
      if (gameState_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(GameState);
      }
      if (PlayerName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(PlayerName);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (gameState_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(GameState);
      }
      if (PlayerName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PlayerName);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PlayerTurn other) {
      if (other == null) {
        return;
      }
      if (other.gameState_ != null) {
        if (gameState_ == null) {
          GameState = new global::MM26.IO.Models.GameState();
        }
        GameState.MergeFrom(other.GameState);
      }
      if (other.PlayerName.Length != 0) {
        PlayerName = other.PlayerName;
      }
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
          case 10: {
            if (gameState_ == null) {
              GameState = new global::MM26.IO.Models.GameState();
            }
            input.ReadMessage(GameState);
            break;
          }
          case 18: {
            PlayerName = input.ReadString();
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
          case 10: {
            if (gameState_ == null) {
              GameState = new global::MM26.IO.Models.GameState();
            }
            input.ReadMessage(GameState);
            break;
          }
          case 18: {
            PlayerName = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
