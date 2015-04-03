using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(RapidFire))]
[CanEditMultipleObjects]
public class RapidFireEditor : Editor{

    bool showHoming = false;
    bool showProjectile = false;

    string[] colors = {"Red", "Green", "Blue", "Yellow"};
    System.Collections.Generic.Dictionary<string, int> colorDict =
        new System.Collections.Generic.Dictionary<string, int>(){
            {"Red", 0},
            {"Green", 1},
            {"Blue", 2},
            {"Yellow", 3}
        };

    public override void OnInspectorGUI(){
        RapidFire rapid = (RapidFire)target;

        EditorGUILayout.LabelField("Active", rapid.Active.ToString());

        rapid.WeaponDamage = EditorGUILayout.FloatField(
                "Weapon Damage", rapid.WeaponDamage);
        rapid.EnergyCost = EditorGUILayout.FloatField(
                "Energy Cost", rapid.EnergyCost);

        HomingMenu(rapid);

        EditorGUILayout.LabelField("Cooldown Ready", rapid.CooldownReady.ToString());
        CooldownField(rapid);

        ProjectileMenu(rapid);

        ProjectileColor(rapid);

        ProjectileRange(rapid);
    }

    private void ProjectileMenu(RapidFire rapid){
        showProjectile = EditorGUILayout.Foldout(showProjectile, "Projectile");
        if(showProjectile){
            EditorGUI.indentLevel++;

            rapid.ProjectileMass = EditorGUILayout.Slider(
                    new GUIContent(
                        "Mass",
                        "Mass of projectile at zero charge."),
                    rapid.ProjectileMass,
                    0.1f,
                    5f);

            rapid.ProjectileSize = EditorGUILayout.Slider(
                    new GUIContent(
                        "Size",
                        "Size of projectile at zero charge."),
                    rapid.ProjectileSize,
                    0.1f,
                    10f);

            rapid.ProjectileForce = EditorGUILayout.Slider(
                    new GUIContent(
                        "Force",
                        "Force of projectile at zero charge."),
                    rapid.ProjectileForce,
                    0.1f,
                    1000f);

            EditorGUI.indentLevel--;
        }
    }

    private void CooldownField(RapidFire rapid){
        rapid.CooldownTime = EditorGUILayout.Slider(
                new GUIContent(
                    "Cooldown Time",
                    "Time required between rapids, in seconds. Minimum is 0.001s."),
                rapid.CooldownTime,
                rapid.MinimumCooldown,
                1);
    }

    private void HomingMenu(RapidFire rapid){
        showHoming = EditorGUILayout.Foldout(showHoming, "Homing");
        if(showHoming){
            EditorGUI.indentLevel++;
            rapid.ProjectileHomingRadius = EditorGUILayout.FloatField(
                    "Radius", rapid.ProjectileHomingRadius);
            rapid.ProjectileHomingForce = EditorGUILayout.FloatField(
                    "Force", rapid.ProjectileHomingForce);
            EditorGUI.indentLevel--;
        }
    }

    private void ProjectileColor(RapidFire rapid){
        rapid.ProjectileColor = colors[EditorGUILayout.Popup(
                "Projectile Color", colorDict[rapid.ProjectileColor], colors)];
    }

    private void ProjectileRange(RapidFire rapid){
    	rapid.ProjectileRange = EditorGUILayout.Slider(
    		new GUIContent(
    			"Projectile Range",
    			"Maximum range of the projectile."),
    		rapid.ProjectileRange,
    		20f,
    		500f);
    }
}
