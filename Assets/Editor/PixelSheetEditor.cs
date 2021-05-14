using System.Collections;
using System.Collections.Generic;
using Pixel;
using UnityEngine;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine.UI;

namespace Editor
{
    [CustomEditor(typeof(PixelSheet), true)]
    public class PixelSheetEditor : UnityEditor.Editor
    {
        private PixelSheet sheet => target as PixelSheet;
        private List<PixelFrame> frames => sheet.frames;

        public float width => Screen.width * 0.8f;
        private int _index;

        private bool currentSpriteNotNull => frames[_index].sprite != null;

        protected void OnEnable()
        {
            _index = 0;
        }

        /* THIS IS THE MAIN BODY, ALL OTHER FUNCTIONS ARE CALLED FROM HERE
         * START DEBUG HERE!!
         * IMPORTANT: 
         */
        public override void OnInspectorGUI()
        {
            if (sheet == null) return;
         
            GUILayout.BeginVertical();
            StartFlexHorizontal();
            DisplayNewSprite();
            EndFlexHorizontal();
            StartFlexHorizontal();
            DisplaySheetControls();
            EndFlexHorizontal();

            if (frames.Count > 0)
            {
                GUILayout.Space(30);

                if (currentSpriteNotNull)
                    DisplayCurrentFrame();
                DisplayFrameControls();
                GUILayout.Space(30);
                StartFlexHorizontal();
                DisplayIndexControls();
                EndFlexHorizontal();
                GUILayout.Space(30);

                DisplayRemoveFrame();
            }

            GUILayout.EndVertical();

            //base.OnInspectorGUI();
            EditorUtility.SetDirty(sheet);
        }

        private void DisplaySheetControls()
        {
            sheet.loop = EditorGUILayout.Toggle("Looping? ", sheet.loop);
            sheet.frameRate = EditorGUILayout.IntField("Frame Rate: ", sheet.frameRate);
        }

        private void DisplayRemoveFrame()
        {
            if (GUILayout.Button("REMOVE FRAME"))
            {
                // ReSharper disable once StringLiteralTypo
                if (EditorUtility.DisplayDialog("Remove Frame for realsies?", "This will remove the frame. You sure?",
                    "Yes, heck this frame", "I take it all back!!!!!"))
                {
                    _index = sheet.RemoveFrame(_index);
                }
            }
        }

        private void DisplayCurrentFrame()
        {
            Vector2 dims = frames[_index].sprite.rect.size;
            
            Vector2 spriteOrigin = new Vector2(width * 0.5f, 128 + dims.y * 0.5f);
            Rect spriteRect = new Rect();
            spriteRect.size = frames[_index].sprite.rect.size;
            spriteRect.center = spriteOrigin;
            EditorGUI.DrawTextureTransparent(spriteRect, GenerateTextureFromSprite(frames[_index].sprite), ScaleMode.ScaleToFit);

            Vector2 rectAreaStart = new Vector2(width * 0.5f - dims.x * 0.5f, 128 + dims.y);
            pivot = new Vector2((rectAreaStart + frames[_index].sprite.pivot).x, rectAreaStart.y - frames[_index].sprite.pivot.y);
    
            Rect boxRect;
            // Basically, gotta set rect offsets such that
            // Key thing here is that the y value we provide is inverse of y value we want
            boxRect = new Rect(pivot.x, pivot.y, 2, 2);
            EditorGUI.DrawRect(boxRect, Color.cyan);

            PixelBoxProps hurtBox = frames[_index].hurtProps;
            Color hurtColor = new Color(1f, 0f, 0f, 0.39f);
            PixelBoxProps hitBox = frames[_index].hitProps;
            Color hitColor = new Color(0f, 1f, 0f, 0.39f);
            
            if (hurtBox.active)
                DisplayBox(hurtBox, hurtColor);
            if (hitBox.active)
                DisplayBox(hitBox, hitColor);
            
            GUILayout.Space(spriteRect.height * 0.5f + spriteRect.y);
        }

        private Vector2 pivot;

        private void DisplayBox(PixelBoxProps _box, Color color)
        {
            Rect boxRect = new Rect
            {
                size = _box.size, center = new Vector2(pivot.x + _box.center.x, pivot.y - _box.center.y)
            };
            EditorGUI.DrawRect(boxRect, color);
        }


        private void DisplayFrameControls()
        {
            
            GUILayout.Label("Frame Squash");
            frames[_index].appliedSquash = EditorGUILayout.Slider(frames[_index].appliedSquash, -1, 1);
            
            GUILayout.Label("Hit Box Properties");
            frames[_index].hitProps.center = EditorGUILayout.Vector2Field("Center: ", frames[_index].hitProps.center);
            frames[_index].hitProps.size = EditorGUILayout.Vector2Field("Size: ", frames[_index].hitProps.size);
            GUILayout.BeginHorizontal();
            frames[_index].hitProps.value = EditorGUILayout.FloatField("Value: ", frames[_index].hitProps.value);
            frames[_index].hitProps.active = EditorGUILayout.Toggle("Active: ", frames[_index].hitProps.active);
            GUILayout.EndHorizontal();
            frames[_index].hitProps.squash = EditorGUILayout.Slider("Hit Squash", frames[_index].hitProps.squash, -1, 1);
            frames[_index].hitProps.hitStop = EditorGUILayout.Slider("HitStop", frames[_index].hitProps.hitStop, 0, 0.5f);
            
            GUILayout.Label("Hurt Box Properties");
            frames[_index].hurtProps.center = EditorGUILayout.Vector2Field("Center: ", frames[_index].hurtProps.center);
            frames[_index].hurtProps.size = EditorGUILayout.Vector2Field("Size: ", frames[_index].hurtProps.size);
            GUILayout.BeginHorizontal();
            frames[_index].hurtProps.value = EditorGUILayout.FloatField("Value: ", frames[_index].hurtProps.value);
            frames[_index].hurtProps.active = EditorGUILayout.Toggle("Active: ", frames[_index].hurtProps.active);
            GUILayout.EndHorizontal();

            frames[_index].sprite = EditorGUILayout.ObjectField(frames[_index].sprite, typeof(Sprite), false) as Sprite;
        }
        
        // Function sourced from: https://answers.unity.com/questions/953254/display-a-sprite-in-an-editorwindow.html
        Texture2D GenerateTextureFromSprite(Sprite aSprite)
        {
            var rect = aSprite.rect;
            var tex = new Texture2D((int)rect.width, (int)rect.height);
            var data = aSprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
            tex.SetPixels(data);
            tex.Apply(true);
            return tex;
        }


        private void DisplayIndexControls()
        {
            if (GUILayout.Button("Prev Frame") && currentSpriteNotNull)
                _index = ((_index - 1) + frames.Count) % frames.Count;
            
            GUILayout.Label($"{sheet.name}_{_index}");
            
            if (GUILayout.Button("Next Frame") && currentSpriteNotNull)
                _index = (_index + 1) % frames.Count;
            GUILayout.Label($"{_index + 1} / {frames.Count}");
        }
        
        private Object _newSprite;
        private void DisplayNewSprite()
        {
            _newSprite = EditorGUILayout.ObjectField("Add Frame: ", _newSprite, typeof(Sprite), false);
            
            if (_newSprite == null) return;

            if (GUILayout.Button("Add Frame And Clear", EditorStyles.toolbarButton))
            {
                _index = frames.Count;
                sheet.AddFrame(_newSprite as Sprite, _index);
                _newSprite = null;
            }
            if (GUILayout.Button("Add Frame", EditorStyles.toolbarButton))
            {
                _index = frames.Count;
                sheet.AddFrame(_newSprite as Sprite, _index);
            }
        }
        
        
        // Helper Functions
        private void StartFlexHorizontal()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
        }
        private void EndFlexHorizontal()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void StartFlexVertical()
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
        }
        private void EndFlexVertical()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

    }
}