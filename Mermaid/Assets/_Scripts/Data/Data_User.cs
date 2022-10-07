using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using CodeStage.AntiCheat.Storage;
using CodeStage.AntiCheat.ObscuredTypes;
//using Photon.Pun;
//using System;

public class Data_User
{
    const string Ver = "v4";
    public string UserName { get; private set; }
    //[JsonProperty("hexas")]
    //public List<test> MyHexas {get; set;}
    public int UserTeamIndex { get; set; }
    //public Type_HexaArea Group { get; private set; }
    //public List<Data_Hexa_Player> Hexas { get; private set; } = new List<Data_Hexa_Player>();
    // public Data_HexaDeck HexaDeck { get => HexaDecks[UserTeamIndex]; }
    // public List<Data_HexaDeck> HexaDecks { get; private set; } = new List<Data_HexaDeck>();
    public List<int> BeforeHexaLinkDeck { get => BeforeHexaLinkDecks[0]; }
    public List<List<int>> BeforeHexaLinkDecks { get; private set; } = new List<List<int>>();    
    //public List<Data_HexaDeck> HexaLinkDecks { get; private set; } = new List<Data_HexaDeck>();
    //public Data_Box PieceBox { get; private set; } = new Data_Box();
    public int PvP_Rating { get; private set; }
    public int PvP_Ranking { get; private set; }
    public int Gold { get => _gold; private set => _gold = value; }
    public int Cash { get => _cash; private set => _cash = value; }
    public int FreeCash { get => _freecash; private set => _freecash = value; }

    public int Skin { get => _skin; }

    public int Profile { get => _profile; }

    // public List<Item> _items { get; private set; } = new List<Item>();

    // public List<Data_Skin_Operator> HasSkin_Operator { get; private set; } = new List<Data_Skin_Operator>();

    // public Data_Skin_Operator MySkin_Data { get => _currentSkin;  private set => _currentSkin = value; }

    public List<int> Select_Items { get; private set; } = new List<int>();

    public int HexaPass_Point {get => _hexapass_point; private set => _hexapass_point = value; } 

    public int Pvp_Play_Count { get=> _pvpcount; private set=> _pvpcount = value; }

    public int Add_Pvp_Play_Count { get=> _pvpAddCount; private set=> _pvpAddCount = value; }

    public int Piece { get => _piece;  private set => _piece = value; }

    public int UserLevel { get => _userlevel;   private set=> _userlevel = value; }

    public int Exp => _exp;

    //tmp
    public int AdCount;

    // public class test
    // {
    //     [JsonProperty("hexa_id")] 
    //     public int ID;
    // }

    ObscuredInt _gold = 0;

    ObscuredInt _cash = 0;
    ObscuredInt _freecash = 0;

    ObscuredInt _skin = 0;
    ObscuredInt _profile = 0;

    ObscuredInt _hexapass_point = 0;

    ObscuredInt _pvpcount = 0;

    ObscuredInt _pvpAddCount = 0;

    ObscuredInt _piece = 0;

    ObscuredInt _userlevel = 0;

    ObscuredInt _exp = 0;

    //Data_Skin_Operator _currentSkin = null;

    
    public class SaveInfo
    {
        public class Hexa
        {
            public Hexa(int id, int count = 1, int level = 0)
            {
                ID = id;
                Count = count;
                Level = level;
            }

            public int ID;
            public int Count;
            public int Level;
        }

        public int BoxPieceCount = 0;
        public List<int> HexaDeck = new List<int>();
        public List<Hexa> Hexas = new List<Hexa>();
    }

    SaveInfo _saveInfo = new SaveInfo();

    // public void UpdateInfo(Packet_GetUserData.UserDataPacket info)
    // {
    //     List<Data_Hexa_Player> hexasData = new List<Data_Hexa_Player>();
    //     var myHexas = info.MyHexas;
    //     var table = Table_Manager.Instance.GetTables<Table_Hexa_unit>();

    //     for (int i = 0; i < myHexas.Count; i++)
    //     {
    //         var tableHexa = table.Find((tData) => tData.id == myHexas[i].ID);
    //         if (tableHexa == null) continue;

    //         var hexa = new Data_Hexa_Player(
    //             tableHexa,
    //             myHexas[i].Piece,
    //             myHexas[i].Lv_Lobby
    //         );
        
    //         if (hexasData.Find((idx) => idx.ID == hexa.ID) == null)
    //         {
    //             hexasData.Add(hexa);
    //             LogManager.LogHexa("헥사 추가 = " + hexa.ID);
    //         }
    //     }
    //     Hexas = hexasData;
    //     List<Data_HexaDeck> deckDatas = new List<Data_HexaDeck>();
    //     List<Data_HexaDeck> linkDeckDatas = new List<Data_HexaDeck>();
    //     List<List<int>> beforeLinkDatas = new List<List<int>>();
    //     var myDecks = info.MyDecks;


    //     for (int i = 0; i < myDecks.Count; i++)
    //     {
    //         var deck = new List<Data_Hexa_Player>();
    //         for (int n = 0; n < myDecks[i].IDs.Count; n++)
    //         {
    //             var findHexa = hexasData.Find((data)=>data.ID == myDecks[i].IDs[n]);
    //             if (findHexa != null)
    //             {
    //                 deck.Add(findHexa);
    //                 LogManager.LogHexa(i + "번째 덱에 " + findHexa.ID + "추가함");
    //             }
    //         }
    //         var addedDeck = new Data_HexaDeck();
    //         addedDeck.SetHexas(deck);
    //         deckDatas.Add(addedDeck);

    //         var link = new List<int>();
    //         for (int a = 0; a < myDecks[i].LinkIDs.Count; a++)
    //         {
    //             link.Add(myDecks[i].LinkIDs[a]);
    //             LogManager.LogHexa("링크 번호 = " + (myDecks[i].LinkIDs[a]));
    //         }
    //         beforeLinkDatas.Add(link);

    //         var linkDeck = new List<Data_Hexa_Player>();
    //         for (int n = 0; n < myDecks[i].LinkIDs.Count; n++)
    //         {
    //             var findHexa = hexasData.Find((data) => data.ID == myDecks[i].LinkIDs[n]);
    //             if (findHexa != null)
    //             {
    //                 linkDeck.Add(findHexa);
    //             }
    //             else
    //             {
    //                 linkDeck.Add(null);
    //             }
    //         }
    //         var addedLinkDeck = new Data_HexaDeck();
    //         addedLinkDeck.SetHexas(linkDeck);
    //         linkDeckDatas.Add(addedLinkDeck);
    //     }

    //     UserTeamIndex = info.TeamIndex;

    //     HexaDecks = deckDatas;
    //     HexaLinkDecks = linkDeckDatas;

    //     BeforeHexaLinkDecks = beforeLinkDatas;

    //     PvP_Rating = info.PvPRating;
    //     PvP_Ranking = info.PvPRanking;

    //     _gold = info.Gold;
    //     _cash = info.Cash;
    //     _freecash = info.FreeCash;

    //     _items = info.MyItems;
    //     _skin = info.Skin;
    //     _profile = info.Profile;
    //     _piece = info.Piece;
    //     _userlevel = info.Level;
    //     _exp = info.EXP;

    //     Select_Items = info.select_items;

    //     HexaPass_Point = info.HexaPass_Point;

    //     Pvp_Play_Count = info.DailyPlayCount;
    //     Add_Pvp_Play_Count = info.AdditionalDailyPlayCount;

    //     for(int i=0; i<_items.Count; i++)
    //     {
    //         var product_table= Table_Manager.Instance.GetTables<Table_Item_product>().Find((ID)=> ID.id == _items[i].item_id);

    //         if(product_table == null)
    //     {
    //             continue;
    //         }
    //         if(product_table.item_type == Item_Type.Skin)
    //         {
    //             var Skin_info = new Data_Skin_Operator(_items[i].item_id);
    //             HasSkin_Operator.Add(Skin_info);
    //         }
    //     }



    //     if(HasSkin_Operator.Count >0)
    //     {
    //         SetSkin(_skin);
    //     }

    //     Check_SelectItem_Zero();
    // }

    // public void Set(SaveInfo saveInfo)
    // {
    //     _saveInfo = saveInfo;

    //     Set();
    // }

    // public void UpdatePvPData(Packet_PvPEnd.PvPEndPacket info)
    // {
    //     PvP_Rating = info.Rating;
    //     PvP_Ranking = info.Rank;
    // }
    public void SetUserNickName(string name)
    {
        LogManager.LogHexa("닉네임 세팅 = " + name);
        UserName = name;
    }

    // public void Set(Type_HexaArea group)
    // {
    //     Group = group;

    //     bool loadData = false;

    //     if (Type_HexaArea.Player1 == group)
    //     {
    //         var playerName = PlayerPrefs.GetString("PlayerName");
    //         if (playerName == "")
    //         {
    //             string id = System.Guid.NewGuid().ToString();

    //             // PlayerPrefs.SetString("PlayerName", "No Name");
    //             // UserName = "No Name";

    //             PlayerPrefs.SetString("PlayerName", id.Substring(0,6));
    //             UserName = id.Substring(0,6);
    //         }
    //         else
    //         {
    //             UserName = playerName;
    //         }

    //         PhotonNetwork.NickName = playerName;
    //         // List<string> randomName = new List<string>();
    //         // randomName.Add("user1");
    //         // randomName.Add("user2");
    //         // randomName.Add("user3");
    //         // randomName.Add("user24");

    //         // UserName = randomName[Random.Range(0, randomName.Count)];

    //         loadData = Load();
    //     }
    //     else
    //     {
    //         List<string> randomName = new List<string>();
    //         randomName.Add("rocket");
    //         randomName.Add("blackgom");
    //         randomName.Add("seulkilee");
    //         randomName.Add("ruby");
    //         randomName.Add("kuroki");
    //         randomName.Add("bimari");
    //         randomName.Add("cs.jung");

    //         UserName = randomName[Random.Range(0, randomName.Count)];
    //     }

    //     if (false == loadData)
    //     {
    //         if (Type_HexaArea.Player1 == group)
    //         {
    //             _saveInfo.Hexas.Clear();
    //             _saveInfo.Hexas.Add(new SaveInfo.Hexa(1));
    //             _saveInfo.Hexas.Add(new SaveInfo.Hexa(2));
    //             _saveInfo.Hexas.Add(new SaveInfo.Hexa(300));
    //             _saveInfo.Hexas.Add(new SaveInfo.Hexa(301));
    //             _saveInfo.Hexas.Add(new SaveInfo.Hexa(600));

    //             // _saveInfo.Hexas.Clear();
    //             // var hexas = Table_Manager.Instance.GetTables<Table_Character>();
    //             // for (int i = 0; i < hexas.Count; i++)
    //             // {
    //             //     _saveInfo.Hexas.Add(new SaveInfo.Hexa(hexas[i].id));
    //             // }

    //             // _saveInfo.HexaDeck.Clear();
    //             // _saveInfo.HexaDeck.Add(_saveInfo.Hexas[0].ID);
    //             // _saveInfo.HexaDeck.Add(_saveInfo.Hexas[1].ID);
    //             // _saveInfo.HexaDeck.Add(_saveInfo.Hexas[2].ID);
    //             // _saveInfo.HexaDeck.Add(_saveInfo.Hexas[3].ID);
    //             // _saveInfo.HexaDeck.Add(_saveInfo.Hexas[4].ID);
    //         }
    //         else
    //         {
    //             _saveInfo.Hexas.Clear();

    //             //var hexas = Table_Manager.Instance.GetTables<Table_Character>();
    //             var hexas = Table_Manager.Instance.GetTables<Table_Hexa_unit>();
    //             for (int i = 0; i < hexas.Count; i++)
    //             {
    //                 _saveInfo.Hexas.Add(new SaveInfo.Hexa(hexas[i].id));
    //             }


    //             _saveInfo.HexaDeck.Clear();
    //             while (GameDefine.MaxHexaDeck > _saveInfo.HexaDeck.Count)
    //             {
    //                 var hexaInfo = _saveInfo.Hexas[Random.Range(0, _saveInfo.Hexas.Count)];
    //                 if (0 > _saveInfo.HexaDeck.FindIndex((checkHexaID) => checkHexaID.Equals(hexaInfo.ID)))
    //                 {
    //                     _saveInfo.HexaDeck.Add(hexaInfo.ID);
    //                 }
    //             }
    //         }

    //         _saveInfo.BoxPieceCount = 0;
    //     }

    //     Set();
    //     Save();
    // }

    // void Set()
    // {
    //     //var hexas = Table_Manager.Instance.GetTables<Table_Character>();
    //     var hexas = Table_Manager.Instance.GetTables<Table_Hexa_unit>();
        
    //     for (int i = 0; i < _saveInfo.Hexas.Count; i++)
    //     {
    //         var hexa = new Data_Hexa_Player(
    //             hexas.Find((checkHexa) => checkHexa.id == _saveInfo.Hexas[i].ID),
    //             _saveInfo.Hexas[i].Count, _saveInfo.Hexas[i].Level);


    //         if (Hexas.Find((idx) => idx.ID == hexa.ID) == null)
    //         {
    //             Hexas.Add(hexa);
    //         }
    //     }
    //     HexaDeck.Set(Hexas);

    //     List<int> hexaIds = new List<int>();
    //     for (int i = 0; i < hexas.Count; i++)
    //     {
    //         hexaIds.Add(hexas[i].id);
    //     }
    //     PieceBox.Set(_saveInfo.BoxPieceCount, hexaIds);
    // }

    // public void DeckCheck()
    // {
    //     while (true == IsDeckEmpty())
    //     {
    //         var randomHexa = Hexas[Random.Range(0, Hexas.Count)];

    //         bool find = false;

    //         for (int i = 0; i < HexaDeck.Hexas.Count; i++)
    //         {
    //             if (null != HexaDeck.Hexas[i])
    //             {
    //                 if (HexaDeck.Hexas[i].ID == randomHexa.ID)
    //                 {
    //                     find = true;
    //                     break;
    //                 }
    //             }
    //         }

    //         if (false == find)
    //         {
    //             for (int i = 0; i < HexaDeck.Hexas.Count; i++)
    //             {
    //                 if (null == HexaDeck.Hexas[i])
    //                 {
    //                     HexaDeck.Hexas[i] = randomHexa;
    //                     break;
    //                 }
    //             }
    //         }
    //     }

    //     Save();
    // }

    // public void AddHexa(int id, int count)
    // {
    //     var hexa = GetHexa(id);
    //     if (null == hexa)
    //     {
    //         var newHexa = new Data_Hexa_Player(
    //                         // Table_Manager.Instance.GetTable<Table_Character>(id),
    //                         Table_Manager.Instance.GetTable<Table_Hexa_unit>(id),
    //                         count);

    //         Hexas.Add(newHexa);
    //         Debug.Log("헥사 추가됨:"+ newHexa.ID);
    //     }
    //     else
    //     {
    //         hexa.SetPiece(hexa.Piece + count);
    //     }
    // }

    // bool IsDeckEmpty()
    // {
    //     bool empty = false;
    //     for (int i = 0; i < HexaDeck.Hexas.Count; i++)
    //     {
    //         if (null == HexaDeck.Hexas[i])
    //         {
    //             empty = true;
    //         }
    //     }

    //     return empty;
    // }

    // public Data_Hexa_Player GetHexa(int id)
    // {
    //     var hexa =  Hexas.Find((hexa) => hexa.ID == id);

    //     // if(hexa == null && id !=0)
    //     // {
    //     //     hexa = new Data_Hexa_Player(id, 0 ,0);
    //     // }
    //     return hexa;
    // }
    // public int IsUserHexas_Contains(int hexaid)
    // {
    //     int isContain = -1;
    //     for(int i=0; i<Hexas.Count; i++)
    //     {
    //         if(Hexas[i].ID == hexaid)
    //         {
    //             isContain = 1;
    //             break;
    //         }
    //     }

    //     return isContain;
    // }
    // public int IsHexaDeck_Contains(int teamindex, int hexaid)
    // {
    //     int isContain = 0;

    //     isContain =HexaDecks[teamindex].Hexas.IndexOf(GetHexa(hexaid));

    //         isContain = isContain == -1 ? -1 : 1;


    //     return isContain;
    // }
    // public int IsChipDeck_Contains(int teamindex, int hexaid)
    // {
    //     int isContain = -1;

    //     var hexa = GetHexa(hexaid);

    //     if(hexa != null)
    //     {
    //     isContain =HexaLinkDecks[teamindex].Hexas.IndexOf(hexa);

    //         isContain = isContain == -1 ? -1 : 1;
    //     }


    //     return isContain;
    // }

    // public List<Data_Hexa_Player> GetHexaList(List<int> ids)
    // {
    //     var hexaList = new List<Data_Hexa_Player>();

    //     for(int i=0; i<ids.Count; i++)
    //     {
    //         if( ids[i] != 0)
    //         {
    //         hexaList.Add(GetHexa(ids[i]));
    //         }
    //         else
    //         {
    //             hexaList.Add(null);
    //         }
    //     }

    //     return hexaList;
    // }

    // public void Save()
    // {
    //     if (Type_HexaArea.Player1 == Group)
    //     {
    //         _saveInfo.BoxPieceCount = PieceBox.PieceCount;

    //         bool empty = false;
    //         for (int i = 0; i < HexaDeck.Hexas.Count; i++)
    //         {
    //             if (null == HexaDeck.Hexas[i])
    //             {
    //                 empty = true;
    //                 break;
    //             }
    //         }

    //         string myDeckHexas = "";
    //         if (true == empty)
    //         {
    //             LogManager.LogHexa("Empty Deck!");
    //         }
    //         else
    //         {
    //             _saveInfo.HexaDeck.Clear();
                
    //             for (int i = 0; i < HexaDeck.Hexas.Count; i++)
    //             {
    //                 _saveInfo.HexaDeck.Add(HexaDeck.Hexas[i].ID);
    //                 myDeckHexas += HexaDeck.Hexas[i].ID + ",";
    //             }
    //         }

    //         _saveInfo.Hexas.Clear();
    //         for (int i = 0; i < Hexas.Count; i++)
    //         {
    //             _saveInfo.Hexas.Add(new SaveInfo.Hexa(Hexas[i].ID, Hexas[i].Piece, Hexas[i].Lv_Lobby));
    //         }

    //         var jsonString = JsonConvert.SerializeObject(_saveInfo);
    //         ObscuredPrefs.SetString(Ver + Group.ToString(), jsonString);
    //         // if (myDeckHexas != "") PlayerPrefs.SetString("SavedMyDeck", myDeckHexas);
    //     }
    // }

    // bool Load()
    // {
    //     if (Type_HexaArea.Player1 == Group)
    //     {
    //         try
    //         {
    //             var jsonString = ObscuredPrefs.GetString(Ver + Group.ToString());
    //             if (false == string.IsNullOrEmpty(jsonString))
    //             {
    //                 _saveInfo = JsonConvert.DeserializeObject<SaveInfo>(jsonString);
    //                 return true;
    //             }
    //         }
    //         catch
    //         {
    //             _saveInfo = new SaveInfo();
    //         }
    //     }

    //     return false;
    // }

    // public void SetTeamInfo(int teamindex, List<int> hexaList, List<int> linkedList)
    // {
    //     //Debug.Log("TeamInfo:"+teamindex);
    //     UserTeamIndex = teamindex;

    //     HexaDecks[teamindex].Hexas.Clear();
    //     HexaLinkDecks[teamindex].Hexas.Clear();

    //     HexaDecks[teamindex].Hexas.AddRange(GetHexaList(hexaList));
    //     HexaLinkDecks[teamindex].Hexas.AddRange(GetHexaList(linkedList));
    // }

    // public void SetHexaInfo(int hexa_id , int level, int part)
    // {
    //     var hexa_Data = GetHexa(hexa_id);

    //     hexa_Data.SetLevel_Lobby(level);
    //     hexa_Data.SetPiece(part);
    // }

    // public void SetGold(int gold)
    // {
    //     _gold =gold;
    // }
    // public void SetCash(int cash)
    // {
    //     _cash = cash;
    // }
    // public void SetFreeCash(int freecash)
    // {
    //     _freecash = freecash;
    // }

    // public void SetSkin(int skin)
    // {
    //     _skin = skin;

    //     for (int i = 0; i < HasSkin_Operator.Count; i++)
    //         {
    //             if (HasSkin_Operator[i].ID == _skin)
    //             {
                    
    //                 _currentSkin = HasSkin_Operator[i];
    //             }
    //             else
    //             {
    //                 continue;
    //             }
    //         }
    // }

    // public void Get_UseItems(Packet_UseItem.UseItem_UserPacket msgData)
    // {
    //     _items = msgData.MyItems;

    //     if(msgData.item_type == Item_Type.Skin)
    //     {
    //         SetSkin(msgData.Skin);
    //     }
    //     else if(msgData.item_type == Item_Type.Profile)
    //     {
    //         _profile = msgData.profile;
    //     }
    // }

    // public  void  SetSelectsItem (List<int> itemslot)
    // {
    //     Select_Items = itemslot;
    // }

    // public void SetServer_Setting(Packet_ServerSetting.Server_Setting_Packet msgData)
    // {
    //     Add_Pvp_Play_Count = msgData.Pvp_Daily_playable_count;
    // }

    // public int Get_ItemCount(int itemID)
    // {
    //     int count =0;
    //     var item = _items.Find(target => target.item_id == itemID);

    //     if(item != null)
    //     {
    //        count = item.count;
    //     }

    //     return count;
    // }

    // void Check_SelectItem_Zero()
    // {
    //     bool isChange = false;
    //     for(int i= 0; i<Select_Items.Count; i++)
    //     {
    //         if(Get_ItemCount(Select_Items[i]) <= 0)
    //         {
    //             Select_Items[i] = 0;
    //             isChange = true;
    //         }
    //     }

    //     if( isChange == true)
    //     {
    //          var packet = new NetworkData
    //         {
    //             Event = PacketEvent.Select_InGameItem,
    //             Data = new Packet_Select_ItemSlot
    //             {
    //                 items = Select_Items
    //             }
    //         };
    //         MessageManager.SendMessage(MessageID.Request_Packet, packet);    
    //     }
    // }
}
