using BepInEx;
using BluePrinceArchipelago.Archipelago;
using BluePrinceArchipelago.ModRooms;
using BluePrinceArchipelago.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BluePrinceArchipelago
{
    internal class ModInstance : MonoBehaviour
    {
        public static Dictionary<string, PlayMakerArrayListProxy> PickerDict = new();
        private static GameObject planPicker = new();
        public ModInstance(IntPtr ptr) : base(ptr)
        {
        }
        private void Start()
        {
            SceneManager.sceneLoaded += (Action<Scene, LoadSceneMode>)OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Plugin.BepinLogger.LogMessage($"Scene: {scene.name} loaded in {mode}");
            if (scene.name.Equals("Mount Holly Estate"))
            {
                planPicker = GameObject.Find("PLAN PICKER").gameObject;
                LoadArrays();
                InitializeRooms();
                Plugin.BepinLogger.LogMessage($"{Plugin.ModRoomManager.Rooms.Count}");
                Plugin.ModRoomManager.Intialize();
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= (Action<Scene, LoadSceneMode>)OnSceneLoaded;
        }

        private void OnGUI()
        {
            // show the mod is currently loaded in the corner
            GUI.Label(new Rect(16, 16, 300, 20), Plugin.ModDisplayInfo);
            ArchipelagoConsole.OnGUI();

            string statusMessage;
            // show the Archipelago Version and whether we're connected or not
            if (ArchipelagoClient.Authenticated)
            {
                // if your game doesn't usually show the cursor this line may be necessary
                // Cursor.visible = false;

                statusMessage = " Status: Connected";
                GUI.Label(new Rect(16, 50, 300, 20), Plugin.APDisplayInfo + statusMessage);
            }
            else
            {
                // if your game doesn't usually show the cursor this line may be necessary
                // Cursor.visible = true;

                statusMessage = " Status: Disconnected";
                GUI.Label(new Rect(16, 50, 300, 20), Plugin.APDisplayInfo + statusMessage);
                GUI.Label(new Rect(16, 70, 150, 20), "Host: ");
                GUI.Label(new Rect(16, 90, 150, 20), "Player Name: ");
                GUI.Label(new Rect(16, 110, 150, 20), "Password: ");

                ArchipelagoClient.ServerData.Uri = GUI.TextField(new Rect(150, 70, 150, 20),
                    ArchipelagoClient.ServerData.Uri);
                ArchipelagoClient.ServerData.SlotName = GUI.TextField(new Rect(150, 90, 150, 20),
                    ArchipelagoClient.ServerData.SlotName);
                ArchipelagoClient.ServerData.Password = GUI.TextField(new Rect(150, 110, 150, 20),
                    ArchipelagoClient.ServerData.Password);

                // requires that the player at least puts *something* in the slot name
                if (GUI.Button(new Rect(16, 130, 100, 20), "Connect") &&
                    !ArchipelagoClient.ServerData.SlotName.IsNullOrWhiteSpace())
                {
                    Plugin.ArchipelagoClient.Connect();
                }
            }
            // this is a good place to create and add a bunch of debug buttons
        }
        private static void LoadArrays() {
            List<int> childIDs = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 55, 56, 58, 59, 60, 61 };

            for (int i = 0; i < childIDs.Count; i++) {
                PlayMakerArrayListProxy array = planPicker.transform.GetChild(childIDs[i]).gameObject.GetComponent<PlayMakerArrayListProxy>();
                PickerDict[array.name.Trim()] = array;
            }
            
        }
        private static void InitializeRooms()
        {
            Plugin.BepinLogger.LogMessage("Initializing Rooms");

            if (Plugin.ModRoomManager == null)
            {
                Plugin.ModRoomManager.AddRoom("AQUARIUM", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CENTER - Tier 2 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("ARCHIVES", new List<string>() { "CENTER - Tier 2" }, true);
                Plugin.ModRoomManager.AddRoom("ATTIC", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - RARE G", "CENTER - Tier 3 G", "EDGECREEP - RARE G", "EDGEPIERCE - RARE G" }, true);
                Plugin.ModRoomManager.AddRoom("BALLROOM", new List<string>() { "FRONTBACK G - RARE", "CENTER - Tier 2 G", "EDGECREEP - RARE G" }, true);
                Plugin.ModRoomManager.AddRoom("BEDROOM", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("BILLIARD ROOM", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 2", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("BOILER ROOM", new List<string>() { "CENTER - Tier 2 G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G" }, true);
                Plugin.ModRoomManager.AddRoom("BOOKSHOP", new List<string>() { "" }, true, false);
                Plugin.ModRoomManager.AddRoom("BOUDOIR", new List<string>() { "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 2", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("BUNK ROOM", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - RARE", "CENTER - Tier 2", "EDGECREEP - RARE", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("CASINO", new List<string>() { "FRONTBACK G - RARE", "EDGEPIERCE G", "EDGE ADVANCE EASTWING - G", "EDGE ADVANCE WESTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "NORTH PIERCE G", "CENTER - Tier 1 G", "CORNER - Tier 1 G" }, false);
                Plugin.ModRoomManager.AddRoom("CHAMBER OF MIRRORS", new List<string>() { "CENTER - Tier 2" }, true);
                Plugin.ModRoomManager.AddRoom("CHAPEL", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("CLASSROOM", new List<string>() { "CENTER - Tier 1 G", "FRONT - Tier 1 G", "CORNER - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("CLOCK TOWER", new List<string>() { "CENTER - Tier 2 G", "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - Tier 1 G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, false);
                Plugin.ModRoomManager.AddRoom("CLOISTER", new List<string>() { "CENTER - Tier 2 G" }, true);
                Plugin.ModRoomManager.AddRoom("CLOSED EXHIBIT", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "EDGEPIERCE - RARE", "EDGECREEP - RARE", "CENTER - Tier 2" }, false);
                Plugin.ModRoomManager.AddRoom("CLOSET", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("COAT CHECK", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("COMMISSARY", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - Tier 1 G", "CENTER - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("CONFERENCE ROOM", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CENTER - Tier 2", "EDGECREEP - RARE", "EDGEPIERCE - RARE" }, true);
                Plugin.ModRoomManager.AddRoom("CONSERVATORY", new List<string>() { "CORNER - Tier 1 G" }, true);
                Plugin.ModRoomManager.AddRoom("CORRIDOR", new List<string>() { "FRONTBACK - RARE", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST" }, true);
                Plugin.ModRoomManager.AddRoom("COURTYARD", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CENTER - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("DARKROOM", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("DEN", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("DINING ROOM", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CENTER - Tier 1", "EDGECREEP - RARE", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("DORMITORY", new List<string>() { "CORNER - Tier 1", "FRONTBACK - RARE", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, false);
                Plugin.ModRoomManager.AddRoom("DOVECOTE", new List<string>() { "EDGEPIERCE EAST", "EDGEPIERCE WEST", "NORTH PIERCE", "CENTER - Tier 2" }, false);
                Plugin.ModRoomManager.AddRoom("DRAFTING STUDIO", new List<string>() { "FRONTBACK G - RARE", "CENTER - Tier 2 G", "EDGECREEP - RARE G" }, true);
                Plugin.ModRoomManager.AddRoom("DRAWING ROOM", new List<string>() { "FRONT - Tier 1 G", "FRONTBACK - RARE", "SOUTH PIERCE", "CENTER - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("EAST WING HALL", new List<string>() { "EDGECREEP EAST", "EDGEPIERCE EAST" }, true);
                Plugin.ModRoomManager.AddRoom("FOYER", new List<string>() { "FRONTBACK G - RARE", "CENTER - Tier 2 G", "EDGECREEP - RARE G" }, true);
                Plugin.ModRoomManager.AddRoom("FURNACE", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CORNER - RARE", "CENTER - Tier 3", "EDGECREEP - RARE", "EDGEPIERCE - RARE" }, true);
                Plugin.ModRoomManager.AddRoom("FREEZER", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - RARE G", "CENTER - Tier 3 G", "EDGECREEP - RARE G", "EDGEPIERCE - RARE G" }, true);
                Plugin.ModRoomManager.AddRoom("GALLERY", new List<string>() { "FRONTBACK - RARE", "CENTER - Tier 3", "EDGECREEP - RARE" }, false);
                Plugin.ModRoomManager.AddRoom("GARAGE", new List<string>() { "EDGE ADVANCE WESTWING - G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("GIFT SHOP", new List<string>() { "CENTER - Tier 2", "FRONT - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("GREAT HALL", new List<string>() { "CENTER - Tier 3" }, true);
                Plugin.ModRoomManager.AddRoom("GREENHOUSE", new List<string>() { "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G" }, true);
                Plugin.ModRoomManager.AddRoom("GUEST BEDROOM", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("GYMNASIUM", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CENTER - Tier 1", "EDGECREEP - RARE", "EDGEPIERCE - RARE" }, true);
                Plugin.ModRoomManager.AddRoom("HALLWAY", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CENTER - Tier 1" }, true);
                Plugin.ModRoomManager.AddRoom("HER LADYSHIP'S CHAMBER", new List<string>() { "EDGE RETREAT WESTWING -  G" }, true);
                Plugin.ModRoomManager.AddRoom("HOVEL", new List<string>() { "STANDALONE ARRAY", "STANDALONE ARRAY FULL" }, true);
                Plugin.ModRoomManager.AddRoom("KITCHEN", new List<string>() { "FRONT - Tier 1 G", "NORTH PIERCE G", "CORNER - Tier 1 G", "CENTER - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("LABORATORY", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - Tier 1 G", "CENTER - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("LAUNDRY ROOM", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - RARE G", "CENTER - Tier 3 G", "EDGECREEP - RARE G", "EDGEPIERCE - RARE G" }, true);
                Plugin.ModRoomManager.AddRoom("LAVATORY", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("LIBRARY", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CORNER - RARE", "CENTER - Tier 2", "EDGECREEP - RARE", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("LOCKER ROOM", new List<string>() { "FRONT - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING - G", "EDGE RETREAT EASTWING - G", "CENTER - Tier 2 G" }, false);
                Plugin.ModRoomManager.AddRoom("LOCKSMITH", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - RARE G", "CENTER - Tier 3 G", "EDGECREEP - RARE G", "EDGEPIERCE - RARE G" }, true);
                Plugin.ModRoomManager.AddRoom("LOST & FOUND", new List<string>() { "FRONTBACK - RARE", "CORNER - Tier 1", "EDGECREEP WEST", "EDGECREEP EAST", "EDGEPIERCE WEST", "EDGEPIERCE EAST", "SOUTH PIERCE", "CENTER - Tier 2" }, true);
                Plugin.ModRoomManager.AddRoom("MAID'S CHAMBER", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CORNER - RARE", "CENTER - Tier 2", "EDGECREEP - RARE", "EDGEPIERCE - RARE" }, true);
                Plugin.ModRoomManager.AddRoom("MAIL ROOM", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CORNER - RARE", "CENTER - Tier 3", "EDGECREEP - RARE", "EDGEPIERCE - RARE" }, true);
                Plugin.ModRoomManager.AddRoom("MASTER BEDROOM", new List<string>() { "EDGE ADVANCE EASTWING - G", "EDGE RETREAT EASTTWING -  G" }, true);
                Plugin.ModRoomManager.AddRoom("MECHANARIUM", new List<string>() { "CENTER - Tier 2" }, false);
                Plugin.ModRoomManager.AddRoom("MORNING ROOM", new List<string>() { "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, false, false);
                Plugin.ModRoomManager.AddRoom("MUSIC ROOM", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - RARE G", "CENTER - Tier 3 G", "EDGECREEP - RARE G", "EDGEPIERCE - RARE G" }, true);
                Plugin.ModRoomManager.AddRoom("NOOK", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("NURSERY", new List<string>() { "FRONT - Tier 1 G", "NORTH PIERCE G", "CORNER - Tier 1 G", "CENTER - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("OBSERVATORY", new List<string>() { "FRONT - Tier 1 G", "NORTH PIERCE G", "CORNER - Tier 1 G", "CENTER - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("OFFICE", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - RARE G", "CENTER - Tier 2 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G", "Center Rare G" }, true);
                Plugin.ModRoomManager.AddRoom("PANTRY", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("PARLOR", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("PASSAGEWAY", new List<string>() { "CENTER - Tier 1 G" }, true);
                Plugin.ModRoomManager.AddRoom("PATIO", new List<string>() { "EDGE ADVANCE WESTWING - G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("PLANETARIUM", new List<string>() { "CENTER - Tier 2", "FRONT - Tier 1", "CORNER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST", "NORTH PIERCE" }, false);
                Plugin.ModRoomManager.AddRoom("PUMP ROOM", new List<string>() { "FRONTBACK - RARE", "CORNER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST", "NORTH PIERCE", "CENTER - Tier 2" }, true, false);
                Plugin.ModRoomManager.AddRoom("ROOM 8", new List<string>() { "DOWSING NOMBOS" }, false, false);
                Plugin.ModRoomManager.AddRoom("ROOT CELLAR", new List<string>() { "STANDALONE ARRAY", "STANDALONE ARRAY FULL" }, true);
                Plugin.ModRoomManager.AddRoom("ROTUNDA", new List<string>() { "CENTER - Tier 2 G" }, true);
                Plugin.ModRoomManager.AddRoom("RUMPUS ROOM", new List<string>() { "FRONTBACK G - RARE", "CENTER - Tier 2 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "Center Rare G" }, true);
                Plugin.ModRoomManager.AddRoom("SAUNA", new List<string>() { "CENTER - Tier 1", "FRONT - Tier 1", "CORNER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST", "NORTH PIERCE" }, true, false);
                Plugin.ModRoomManager.AddRoom("SCHOOLHOUSE", new List<string>() { "STANDALONE ARRAY", "STANDALONE ARRAY FULL" }, true);
                Plugin.ModRoomManager.AddRoom("SECRET GARDEN", new List<string>() { "" }, true, false);
                Plugin.ModRoomManager.AddRoom("SECRET PASSAGE", new List<string>() { "CENTER - Tier 2 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "Center Rare G" }, true);
                Plugin.ModRoomManager.AddRoom("SECURITY", new List<string>() { "NORTH PIERCE G", "CENTER - Tier 1 G", "EDGEPIERCE G" }, true);
                Plugin.ModRoomManager.AddRoom("SERVANT'S QUARTERS", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - RARE G", "CENTER - Tier 2 G", "EDGECREEP - RARE G", "EDGEPIERCE - RARE G" }, true);
                Plugin.ModRoomManager.AddRoom("BOMB SHELTER", new List<string>() { "STANDALONE ARRAY", "STANDALONE ARRAY FULL" }, true);
                Plugin.ModRoomManager.AddRoom("SHOWROOM", new List<string>() { "FRONTBACK G - RARE", "CENTER - Tier 3 G", "EDGECREEP - RARE G", "SILVER KEY LIST 2", "Center Rare G" }, true);
                Plugin.ModRoomManager.AddRoom("SHRINE", new List<string>() { "STANDALONE ARRAY", "STANDALONE ARRAY FULL" }, true);
                Plugin.ModRoomManager.AddRoom("SOLARIUM", new List<string>() { "CORNER - RARE G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G", "NORTH PIERCE G", "CENTER - Tier 2 G" }, true);
                Plugin.ModRoomManager.AddRoom("SPARE ROOM", new List<string>() { "FRONTBACK - RARE", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST" }, true);
                Plugin.ModRoomManager.AddRoom("STOREROOM", new List<string>() { "FRONTBACK - RARE", "SOUTH PIERCE", "CORNER - Tier 1", "CENTER - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("STUDY", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CORNER - RARE", "CENTER - Tier 2", "EDGECREEP - RARE", "EDGEPIERCE - RARE", "Center Rare" }, true);
                Plugin.ModRoomManager.AddRoom("TERRACE", new List<string>() { "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("THE ARMORY", new List<string>() { "CENTER - Tier 1 G", "CORNER - Tier 1 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G", "NORTH PIERCE G" }, false, false);
                Plugin.ModRoomManager.AddRoom("THE FOUNDATION", new List<string>() { "CENTER - Tier 1", "CENTER - Tier 2", "CENTER - Tier 3" }, true);
                Plugin.ModRoomManager.AddRoom("THE KENNEL", new List<string>() { "FRONT - Tier 1", "EDGECREEP EAST", "EDGECREEP WEST", "CENTER - Tier 1" }, true);
                Plugin.ModRoomManager.AddRoom("THE POOL", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CENTER - Tier 2 G", "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G", "EDGEPIERCE G", "Center Rare G" }, true);
                Plugin.ModRoomManager.AddRoom("THRONE ROOM", new List<string>() { "EDGEPIERCE - RARE G", "CENTER - Tier 2 G" }, false);
                Plugin.ModRoomManager.AddRoom("TOMB", new List<string>() { "STANDALONE ARRAY", "STANDALONE ARRAY FULL" }, true);
                Plugin.ModRoomManager.AddRoom("TOOLSHED", new List<string>() { "STANDALONE ARRAY", "STANDALONE ARRAY FULL" }, true);
                Plugin.ModRoomManager.AddRoom("TRADING POST", new List<string>() { "STANDALONE ARRAY", "STANDALONE ARRAY FULL" }, true);
                Plugin.ModRoomManager.AddRoom("TREASURE TROVE", new List<string>() { "SILVER KEY LIST 2", "LIBRARY LIST", "LIBRARY LIST RARER", "DOWSING NOMBOS", "BLACKPRINTS" }, false);
                Plugin.ModRoomManager.AddRoom("TROPHY ROOM", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - RARE G", "CENTER - Tier 3 G", "EDGECREEP - RARE G", "EDGEPIERCE - RARE G", "Center Rare G" }, true);
                Plugin.ModRoomManager.AddRoom("TUNNEL", new List<string>() { "CENTER - Tier 2", "EDGECREEP EAST", "EDGECREEP WEST" }, false);
                Plugin.ModRoomManager.AddRoom("UTILITY CLOSET", new List<string>() { "FRONTBACK - RARE", "CORNER - Tier 1", "CENTER - Tier 2", "EDGECREEP EAST", "EDGECREEP WEST", "EDGEPIERCE EAST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("VAULT", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - RARE G", "CENTER - Tier 2 G", "EDGECREEP - RARE G", "EDGEPIERCE - RARE G", "Center Rare G" }, true);
                Plugin.ModRoomManager.AddRoom("VERANDA", new List<string>() { "EDGE ADVANCE WESTWING - G", "EDGE ADVANCE EASTWING - G", "EDGE RETREAT WESTWING -  G", "EDGE RETREAT EASTTWING -  G" }, true);
                Plugin.ModRoomManager.AddRoom("VESTIBULE", new List<string>() { "CENTER - Tier 1 G" }, false);
                Plugin.ModRoomManager.AddRoom("WALK-IN CLOSET", new List<string>() { "FRONTBACK G - RARE", "NORTH PIERCE G", "CORNER - Tier 1 G", "CENTER - Tier 2 G", "EDGECREEP - RARE G", "EDGEPIERCE G", "Center Rare G" }, true);
                Plugin.ModRoomManager.AddRoom("WEIGHT ROOM", new List<string>() { "CENTER - Tier 3", "Center Rare" }, true);
                Plugin.ModRoomManager.AddRoom("WEST WING HALL", new List<string>() { "EDGECREEP WEST", "EDGEPIERCE WEST" }, true);
                Plugin.ModRoomManager.AddRoom("WINE CELLAR", new List<string>() { "FRONTBACK - RARE", "NORTH PIERCE", "CORNER - RARE", "CENTER - Tier 1", "EDGECREEP - RARE", "EDGEPIERCE - RARE" }, true);
                Plugin.ModRoomManager.AddRoom("WORKSHOP", new List<string>() { "FRONTBACK - RARE", "CENTER - Tier 2", "EDGECREEP - RARE", "Center Rare" }, true);
            }
        }
    }
}
