using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(ChargedShot))]
[CanEditMultipleObjects]
public class ChargedShotEditor : Editor{

    bool showHoming = false;
    bool showCharge = false;
    bool showPreCharge = false;

    string[] colors = {"Red", "Green", "Blue", "Yellow"};
    System.Collections.Generic.Dictionary<string, int> colorDict =
        new System.Collections.Generic.Dictionary<string, int>(){
            {"Red", 0},
            {"Green", 1},
            {"Blue", 2},
            {"Yellow", 3}
        };

    public override void OnInspectorGUI(){
        ChargedShot shot = (ChargedShot)target;

        EditorGUILayout.LabelField("Active", shot.Active.ToString());

        EditorGUILayout.LabelField("Cooldown Ready", shot.CooldownReady.ToString());

        CooldownField(shot);

        PreChargeMenu(shot);

        ChargeMenu(shot);
        EditorGUILayout.LabelField("Charging", shot.Charging.ToString());

        HomingMenu(shot);

        ProjectileColor(shot);
    }

    private void PreChargeMenu(ChargedShot shot){
        showPreCharge = EditorGUILayout.Foldout(showPreCharge, "Pre-charge");
        if(showPreCharge){
            EditorGUI.indentLevel++;
            
            shot.EnergyCost = EditorGUILayout.FloatField(
                    "Energy Cost", shot.EnergyCost);


            shot.WeaponDamage = EditorGUILayout.FloatField(
                    "Weapon Damage", shot.WeaponDamage);

            shot.ProjectileMass = EditorGUILayout.Slider(
                    new GUIContent(
                        "Mass",
                        "Mass of projectile at zero charge."),
                    shot.ProjectileMass,
                    0.1f,
                    5f);

            shot.ProjectileSize = EditorGUILayout.Slider(
                    new GUIContent(
                        "Size",
                        "Size of projectile at zero charge."),
                    shot.ProjectileSize,
                    0.1f,
                    10f);

            shot.ProjectileForce = EditorGUILayout.Slider(
                    new GUIContent(
                        "Force",
                        "Force of projectile at zero charge."),
                    shot.ProjectileForce,
                    0.1f,
                    1000f);

            EditorGUI.indentLevel--;
        }
    }

    private void ChargeMenu(ChargedShot shot){
        showCharge = EditorGUILayout.Foldout(showCharge, "Charge");
        if(showCharge){
            EditorGUI.indentLevel++;

            shot.MaxChargeTime = EditorGUILayout.Slider(
                    new GUIContent(
                        "Max Charge Time",
                        "The maximum effective charge time."),
                    shot.MaxChargeTime,
                    0,
                    10);

            shot.ChargeDamage = EditorGUILayout.Slider(
                    new GUIContent(
                        "Damage",
                        "The amount of damage added at maximum charge."),
                    shot.ChargeDamage,
                    0,
                    100);

            shot.ChargeMass = EditorGUILayout.Slider(
                    new GUIContent(
                        "Mass",
                        "The amount of mass added at maximum charge."),
                    shot.ChargeMass,
                    0,
                    50);

            shot.ChargeSize = EditorGUILayout.Slider(
                    new GUIContent(
                        "Size",
                        "The amount of size added at maximum charge."),
                    shot.ChargeSize,
                    0,
                    10);

            shot.ChargeForce = EditorGUILayout.Slider(
                    new GUIContent(
                        "Force",
                        "The amount of size added at maximum charge."),
                    shot.ChargeForce,
                    0,
                    1000);

            EditorGUI.indentLevel--;
        }
    }

    private void CooldownField(ChargedShot shot){
        shot.CooldownTime = EditorGUILayout.Slider(
                new GUIContent(
                    "Cooldown Time",
                    "Time required between shots, in seconds. Minimum is 0.001s."),
                shot.CooldownTime,
                shot.MinimumCooldown,
                4);
    }

    private void HomingMenu(ChargedShot shot){
        showHoming = EditorGUILayout.Foldout(showHoming, "Homing");
        if(showHoming){
            EditorGUI.indentLevel++;
            shot.ProjectileHomingRadius = EditorGUILayout.FloatField(
                    "Radius", shot.ProjectileHomingRadius);
            shot.ProjectileHomingForce = EditorGUILayout.FloatField(
                    "Force", shot.ProjectileHomingForce);
            EditorGUI.indentLevel--;
        }
    }

    private void ProjectileColor(ChargedShot shot){
        shot.ProjectileColor = colors[EditorGUILayout.Popup(
                "Projectile Color", colorDict[shot.ProjectileColor], colors)];
    }
}
