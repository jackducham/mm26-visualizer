// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: game.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MM26.IO.Models {

  /// <summary>Holder for reflection information generated from game.proto</summary>
  public static partial class GameReflection {

    #region Descriptor
    /// <summary>File descriptor for game.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GameReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgpnYW1lLnByb3RvEgRnYW1lGgtib2FyZC5wcm90bxoPY2hhcmFjdGVyLnBy",
            "b3RvIpYDCglHYW1lU3RhdGUSEAoIc3RhdGVfaWQYASABKAMSNAoLYm9hcmRf",
            "bmFtZXMYAiADKAsyHy5nYW1lLkdhbWVTdGF0ZS5Cb2FyZE5hbWVzRW50cnkS",
            "NgoMcGxheWVyX25hbWVzGAMgAygLMiAuZ2FtZS5HYW1lU3RhdGUuUGxheWVy",
            "TmFtZXNFbnRyeRI4Cg1tb25zdGVyX25hbWVzGAQgAygLMiEuZ2FtZS5HYW1l",
            "U3RhdGUuTW9uc3Rlck5hbWVzRW50cnkaPwoPQm9hcmROYW1lc0VudHJ5EgsK",
            "A2tleRgBIAEoCRIbCgV2YWx1ZRgCIAEoCzIMLmJvYXJkLkJvYXJkOgI4ARpF",
            "ChBQbGF5ZXJOYW1lc0VudHJ5EgsKA2tleRgBIAEoCRIgCgV2YWx1ZRgCIAEo",
            "CzIRLmNoYXJhY3Rlci5QbGF5ZXI6AjgBGkcKEU1vbnN0ZXJOYW1lc0VudHJ5",
            "EgsKA2tleRgBIAEoCRIhCgV2YWx1ZRgCIAEoCzISLmNoYXJhY3Rlci5Nb25z",
            "dGVyOgI4ASLGAQoKR2FtZUNoYW5nZRIYChBuZXdfcGxheWVyX25hbWVzGAEg",
            "AygJEkoKFmNoYXJhY3Rlcl9zdGF0X2NoYW5nZXMYAiADKAsyKi5nYW1lLkdh",
            "bWVDaGFuZ2UuQ2hhcmFjdGVyU3RhdENoYW5nZXNFbnRyeRpSChlDaGFyYWN0",
            "ZXJTdGF0Q2hhbmdlc0VudHJ5EgsKA2tleRgBIAEoCRIkCgV2YWx1ZRgCIAEo",
            "CzIVLmdhbWUuQ2hhcmFjdGVyQ2hhbmdlOgI4ASKFAQoPQ2hhcmFjdGVyQ2hh",
            "bmdlEgwKBGRpZWQYASABKAgSEQoJcmVzcGF3bmVkGAIgASgIEi4KDWRlY2lz",
            "aW9uX3R5cGUYAyABKA4yFy5jaGFyYWN0ZXIuRGVjaXNpb25UeXBlEiEKBHBh",
            "dGgYBCADKAsyEy5jaGFyYWN0ZXIuUG9zaXRpb25CQgoebWVjaC5tYW5pYS5l",
            "bmdpbmUuZG9tYWluLm1vZGVsQg9HYW1lU3RhdGVQcm90b3OqAg5NTTI2LklP",
            "Lk1vZGVsc2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::MM26.IO.Models.BoardReflection.Descriptor, global::MM26.IO.Models.CharacterReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::MM26.IO.Models.GameState), global::MM26.IO.Models.GameState.Parser, new[]{ "StateId", "BoardNames", "PlayerNames", "MonsterNames" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, null, null, }),
            new pbr::GeneratedClrTypeInfo(typeof(global::MM26.IO.Models.GameChange), global::MM26.IO.Models.GameChange.Parser, new[]{ "NewPlayerNames", "CharacterStatChanges" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, }),
            new pbr::GeneratedClrTypeInfo(typeof(global::MM26.IO.Models.CharacterChange), global::MM26.IO.Models.CharacterChange.Parser, new[]{ "Died", "Respawned", "DecisionType", "Path" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class GameState : pb::IMessage<GameState>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GameState> _parser = new pb::MessageParser<GameState>(() => new GameState());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GameState> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MM26.IO.Models.GameReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GameState() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GameState(GameState other) : this() {
      stateId_ = other.stateId_;
      boardNames_ = other.boardNames_.Clone();
      playerNames_ = other.playerNames_.Clone();
      monsterNames_ = other.monsterNames_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GameState Clone() {
      return new GameState(this);
    }

    /// <summary>Field number for the "state_id" field.</summary>
    public const int StateIdFieldNumber = 1;
    private long stateId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long StateId {
      get { return stateId_; }
      set {
        stateId_ = value;
      }
    }

    /// <summary>Field number for the "board_names" field.</summary>
    public const int BoardNamesFieldNumber = 2;
    private static readonly pbc::MapField<string, global::MM26.IO.Models.Board>.Codec _map_boardNames_codec
        = new pbc::MapField<string, global::MM26.IO.Models.Board>.Codec(pb::FieldCodec.ForString(10, ""), pb::FieldCodec.ForMessage(18, global::MM26.IO.Models.Board.Parser), 18);
    private readonly pbc::MapField<string, global::MM26.IO.Models.Board> boardNames_ = new pbc::MapField<string, global::MM26.IO.Models.Board>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<string, global::MM26.IO.Models.Board> BoardNames {
      get { return boardNames_; }
    }

    /// <summary>Field number for the "player_names" field.</summary>
    public const int PlayerNamesFieldNumber = 3;
    private static readonly pbc::MapField<string, global::MM26.IO.Models.Player>.Codec _map_playerNames_codec
        = new pbc::MapField<string, global::MM26.IO.Models.Player>.Codec(pb::FieldCodec.ForString(10, ""), pb::FieldCodec.ForMessage(18, global::MM26.IO.Models.Player.Parser), 26);
    private readonly pbc::MapField<string, global::MM26.IO.Models.Player> playerNames_ = new pbc::MapField<string, global::MM26.IO.Models.Player>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<string, global::MM26.IO.Models.Player> PlayerNames {
      get { return playerNames_; }
    }

    /// <summary>Field number for the "monster_names" field.</summary>
    public const int MonsterNamesFieldNumber = 4;
    private static readonly pbc::MapField<string, global::MM26.IO.Models.Monster>.Codec _map_monsterNames_codec
        = new pbc::MapField<string, global::MM26.IO.Models.Monster>.Codec(pb::FieldCodec.ForString(10, ""), pb::FieldCodec.ForMessage(18, global::MM26.IO.Models.Monster.Parser), 34);
    private readonly pbc::MapField<string, global::MM26.IO.Models.Monster> monsterNames_ = new pbc::MapField<string, global::MM26.IO.Models.Monster>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<string, global::MM26.IO.Models.Monster> MonsterNames {
      get { return monsterNames_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GameState);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GameState other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (StateId != other.StateId) return false;
      if (!BoardNames.Equals(other.BoardNames)) return false;
      if (!PlayerNames.Equals(other.PlayerNames)) return false;
      if (!MonsterNames.Equals(other.MonsterNames)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (StateId != 0L) hash ^= StateId.GetHashCode();
      hash ^= BoardNames.GetHashCode();
      hash ^= PlayerNames.GetHashCode();
      hash ^= MonsterNames.GetHashCode();
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
      if (StateId != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(StateId);
      }
      boardNames_.WriteTo(output, _map_boardNames_codec);
      playerNames_.WriteTo(output, _map_playerNames_codec);
      monsterNames_.WriteTo(output, _map_monsterNames_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (StateId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(StateId);
      }
      size += boardNames_.CalculateSize(_map_boardNames_codec);
      size += playerNames_.CalculateSize(_map_playerNames_codec);
      size += monsterNames_.CalculateSize(_map_monsterNames_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GameState other) {
      if (other == null) {
        return;
      }
      if (other.StateId != 0L) {
        StateId = other.StateId;
      }
      boardNames_.Add(other.boardNames_);
      playerNames_.Add(other.playerNames_);
      monsterNames_.Add(other.monsterNames_);
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
            StateId = input.ReadInt64();
            break;
          }
          case 18: {
            boardNames_.AddEntriesFrom(input, _map_boardNames_codec);
            break;
          }
          case 26: {
            playerNames_.AddEntriesFrom(input, _map_playerNames_codec);
            break;
          }
          case 34: {
            monsterNames_.AddEntriesFrom(input, _map_monsterNames_codec);
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
            StateId = input.ReadInt64();
            break;
          }
          case 18: {
            boardNames_.AddEntriesFrom(ref input, _map_boardNames_codec);
            break;
          }
          case 26: {
            playerNames_.AddEntriesFrom(ref input, _map_playerNames_codec);
            break;
          }
          case 34: {
            monsterNames_.AddEntriesFrom(ref input, _map_monsterNames_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class GameChange : pb::IMessage<GameChange>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GameChange> _parser = new pb::MessageParser<GameChange>(() => new GameChange());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GameChange> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MM26.IO.Models.GameReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GameChange() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GameChange(GameChange other) : this() {
      newPlayerNames_ = other.newPlayerNames_.Clone();
      characterStatChanges_ = other.characterStatChanges_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GameChange Clone() {
      return new GameChange(this);
    }

    /// <summary>Field number for the "new_player_names" field.</summary>
    public const int NewPlayerNamesFieldNumber = 1;
    private static readonly pb::FieldCodec<string> _repeated_newPlayerNames_codec
        = pb::FieldCodec.ForString(10);
    private readonly pbc::RepeatedField<string> newPlayerNames_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> NewPlayerNames {
      get { return newPlayerNames_; }
    }

    /// <summary>Field number for the "character_stat_changes" field.</summary>
    public const int CharacterStatChangesFieldNumber = 2;
    private static readonly pbc::MapField<string, global::MM26.IO.Models.CharacterChange>.Codec _map_characterStatChanges_codec
        = new pbc::MapField<string, global::MM26.IO.Models.CharacterChange>.Codec(pb::FieldCodec.ForString(10, ""), pb::FieldCodec.ForMessage(18, global::MM26.IO.Models.CharacterChange.Parser), 18);
    private readonly pbc::MapField<string, global::MM26.IO.Models.CharacterChange> characterStatChanges_ = new pbc::MapField<string, global::MM26.IO.Models.CharacterChange>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<string, global::MM26.IO.Models.CharacterChange> CharacterStatChanges {
      get { return characterStatChanges_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GameChange);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GameChange other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!newPlayerNames_.Equals(other.newPlayerNames_)) return false;
      if (!CharacterStatChanges.Equals(other.CharacterStatChanges)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= newPlayerNames_.GetHashCode();
      hash ^= CharacterStatChanges.GetHashCode();
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
      newPlayerNames_.WriteTo(output, _repeated_newPlayerNames_codec);
      characterStatChanges_.WriteTo(output, _map_characterStatChanges_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += newPlayerNames_.CalculateSize(_repeated_newPlayerNames_codec);
      size += characterStatChanges_.CalculateSize(_map_characterStatChanges_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GameChange other) {
      if (other == null) {
        return;
      }
      newPlayerNames_.Add(other.newPlayerNames_);
      characterStatChanges_.Add(other.characterStatChanges_);
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
            newPlayerNames_.AddEntriesFrom(input, _repeated_newPlayerNames_codec);
            break;
          }
          case 18: {
            characterStatChanges_.AddEntriesFrom(input, _map_characterStatChanges_codec);
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
            newPlayerNames_.AddEntriesFrom(ref input, _repeated_newPlayerNames_codec);
            break;
          }
          case 18: {
            characterStatChanges_.AddEntriesFrom(ref input, _map_characterStatChanges_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class CharacterChange : pb::IMessage<CharacterChange>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CharacterChange> _parser = new pb::MessageParser<CharacterChange>(() => new CharacterChange());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CharacterChange> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MM26.IO.Models.GameReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CharacterChange() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CharacterChange(CharacterChange other) : this() {
      died_ = other.died_;
      respawned_ = other.respawned_;
      decisionType_ = other.decisionType_;
      path_ = other.path_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CharacterChange Clone() {
      return new CharacterChange(this);
    }

    /// <summary>Field number for the "died" field.</summary>
    public const int DiedFieldNumber = 1;
    private bool died_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Died {
      get { return died_; }
      set {
        died_ = value;
      }
    }

    /// <summary>Field number for the "respawned" field.</summary>
    public const int RespawnedFieldNumber = 2;
    private bool respawned_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Respawned {
      get { return respawned_; }
      set {
        respawned_ = value;
      }
    }

    /// <summary>Field number for the "decision_type" field.</summary>
    public const int DecisionTypeFieldNumber = 3;
    private global::MM26.IO.Models.DecisionType decisionType_ = global::MM26.IO.Models.DecisionType.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::MM26.IO.Models.DecisionType DecisionType {
      get { return decisionType_; }
      set {
        decisionType_ = value;
      }
    }

    /// <summary>Field number for the "path" field.</summary>
    public const int PathFieldNumber = 4;
    private static readonly pb::FieldCodec<global::MM26.IO.Models.Position> _repeated_path_codec
        = pb::FieldCodec.ForMessage(34, global::MM26.IO.Models.Position.Parser);
    private readonly pbc::RepeatedField<global::MM26.IO.Models.Position> path_ = new pbc::RepeatedField<global::MM26.IO.Models.Position>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::MM26.IO.Models.Position> Path {
      get { return path_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CharacterChange);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CharacterChange other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Died != other.Died) return false;
      if (Respawned != other.Respawned) return false;
      if (DecisionType != other.DecisionType) return false;
      if(!path_.Equals(other.path_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Died != false) hash ^= Died.GetHashCode();
      if (Respawned != false) hash ^= Respawned.GetHashCode();
      if (DecisionType != global::MM26.IO.Models.DecisionType.None) hash ^= DecisionType.GetHashCode();
      hash ^= path_.GetHashCode();
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
      if (Died != false) {
        output.WriteRawTag(8);
        output.WriteBool(Died);
      }
      if (Respawned != false) {
        output.WriteRawTag(16);
        output.WriteBool(Respawned);
      }
      if (DecisionType != global::MM26.IO.Models.DecisionType.None) {
        output.WriteRawTag(24);
        output.WriteEnum((int) DecisionType);
      }
      path_.WriteTo(output, _repeated_path_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Died != false) {
        size += 1 + 1;
      }
      if (Respawned != false) {
        size += 1 + 1;
      }
      if (DecisionType != global::MM26.IO.Models.DecisionType.None) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) DecisionType);
      }
      size += path_.CalculateSize(_repeated_path_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CharacterChange other) {
      if (other == null) {
        return;
      }
      if (other.Died != false) {
        Died = other.Died;
      }
      if (other.Respawned != false) {
        Respawned = other.Respawned;
      }
      if (other.DecisionType != global::MM26.IO.Models.DecisionType.None) {
        DecisionType = other.DecisionType;
      }
      path_.Add(other.path_);
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
            Died = input.ReadBool();
            break;
          }
          case 16: {
            Respawned = input.ReadBool();
            break;
          }
          case 24: {
            DecisionType = (global::MM26.IO.Models.DecisionType) input.ReadEnum();
            break;
          }
          case 34: {
            path_.AddEntriesFrom(input, _repeated_path_codec);
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
            Died = input.ReadBool();
            break;
          }
          case 16: {
            Respawned = input.ReadBool();
            break;
          }
          case 24: {
            DecisionType = (global::MM26.IO.Models.DecisionType) input.ReadEnum();
            break;
          }
          case 34: {
            path_.AddEntriesFrom(ref input, _repeated_path_codec);
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