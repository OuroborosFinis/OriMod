using System;
using OriMod.Abilities;
using Terraria;

namespace OriMod.Projectiles.Abilities {
  /// <summary>
  /// Projectile hitbox for when the player is using <see cref="ChargeJump"/> and <see cref="WallChargeJump"/>.
  /// </summary>
  public sealed class ChargeJumpProjectile : AbilityProjectile {
    /// <summary>
    /// This is not used, as this <see cref="AbilityProjectile"/> is used by both <see cref="ChargeJump"/> and <see cref="WallChargeJump"/>.
    /// </summary>
    public override byte abilityID => AbilityID.ChargeJump;

    public override void SetDefaults() {
      base.SetDefaults();
      projectile.width = 96;
      projectile.height = 96;
    }

    public override void CheckAbilityActive() {
      if (oPlayer.abilities.chargeJump || oPlayer.abilities.wallChargeJump) {
        projectile.timeLeft = 2;
      }
    }

    public override void Behavior() {
      base.Behavior();
      // Stretch projectile size based on velocity
      var vel = oPlayer.player.velocity;
      projectile.width = (int)Utils.Clamp(Math.Abs(vel.X) * 2.5f, 96, 250);
      projectile.height = (int)Utils.Clamp(Math.Abs(vel.Y) * 2.5f, 96, 250);
    }
  }
}
