using System;
using Terraria.GameInput;

namespace OriMod.Abilities {
  /// <summary>
  /// Ability for looking up. Pairs with the ability <see cref="ChargeJump"/>.
  /// <para>This ability on its own is entirely visual, and is always unlocked.</para>
  /// </summary>
  public sealed class LookUp : Ability {
    internal LookUp(AbilityManager manager) : base(manager) { }
    public override int Id => AbilityID.LookUp;
    public override bool Unlocked => true;

    internal override bool UpdateCondition => PlayerInput.Triggers.Current.Up;
    internal override bool CanUse => base.CanUse && oPlayer.IsGrounded && Math.Abs(player.velocity.X) < 0.8f && !Manager.crouch.InUse && !Manager.dash.InUse && !Manager.chargeDash.InUse;

    private int StartDuration => 12;
    private int EndDuration => 8;

    internal override void Tick() {
      if (!InUse) {
        if (CanUse && (PlayerInput.Triggers.Current.Up || OriMod.ChargeKey.Current)) {
          SetState(State.Starting);
        }
      }
      else if (!CanUse) {
        SetState(State.Inactive);
      }
      else if (!(PlayerInput.Triggers.Current.Up || OriMod.ChargeKey.Current) && !Ending) {
        if (Active) {
          SetState(State.Ending);
        }
        else {
          SetState(State.Inactive);
        }
        return;
      }
      else if (Starting) {
        if (CurrentTime > StartDuration) {
          SetState(State.Active);
        }
      }
      else if (Ending) {
        if (CurrentTime > EndDuration) {
          SetState(State.Inactive);
        }
      }
    }
  }
}
