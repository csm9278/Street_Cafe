using UnityEngine;

public class GlobalData
{
    public enum Drink
    {
        Water,
        Americano,
        GreenTea,
        CafeLatte,
        GreenTeaLatte,
        ColdBrue,
        ColdBrueLatte,
        BlackTea,
        BlackTeaLatte,
    }

    public enum DrinkBase
    {
        Water,
        Milk,
        SoyMilk
    }

    public enum IceHot
    {
        Hot,
        Ice,
        None
    }

    public enum AmountIce
    {
        None,
        Less,
        Medium,
        Many
    }

    public enum AdditiveItem
    {
        None,
        Sirup,
        Sugar,
        Honey
    }

    public enum AmountAdditive
    {
        None,
        Less,
        Medium,
        Many
    }

    public enum UpgradeState
    {
        None = 0,
        Milk = 1,
        ColdBrue,
        TrashCan,
        Sirup,
        BlackTea,
        ViewOrder
    }

    public class Order
    {
        public Drink drinktype;
        public IceHot iceHotType;
        public AmountIce amountIce;
        public DrinkBase drinkBase = DrinkBase.Water;
        public AdditiveItem additiveItem = AdditiveItem.None;   //일단 시럽 횟수로 하자
        public AmountAdditive amountAdditive = AmountAdditive.None;
        public bool packaged = false;

        public int customerKind = 0;
        public Order(Drink drink, IceHot iceHot, AmountIce ice, DrinkBase drinkB = DrinkBase.Water, AdditiveItem additive = AdditiveItem.None, int custKind = 0)
        {
            drinktype = drink;
            iceHotType = iceHot;
            amountIce = ice;
            drinkBase = drinkB;
            additiveItem = additive;
            customerKind = custKind;
        }

        public void Reset()
        {
            drinktype = Drink.Water;
            iceHotType = IceHot.None;
            amountIce = AmountIce.None;
            drinkBase = DrinkBase.Water;
            additiveItem = AdditiveItem.None;
            packaged = false;
        }
    }

    public static string[] firstOrderText = { "안녕하세요! ", "이봐 ", "음.. ", "", "", "안녕 ", "늘 먹던 " };
    public static string[] icehotOrderText = { "따듯한 ", "아이스 ", ""};
    public static string[] menuOrderText = {"물 한잔 ", "아메리카노 한잔 ", "녹차 한잔 ", "카페 라떼 ", "녹차 라떼 ", "콜드브루 한잔 ", "콜드브루 라떼 ", "블랙티 ", "블랙티 라떼 " };
    public static string[] menuNameText = {"물", "아메리카노", "녹차", "카페 라떼", "녹차 라떼", "콜드브루",  "콜드브루 라떼", "블랙티", "블랙티 라떼" };
    public static string[] iceOrderText = {"얼음 빼고 ", "얼음 적게 "," ","얼음 많이 " };
    public static string[] iceNameText = {"얼음 없음", "얼음 적게","얼음 적당히","얼음 많이" };
    public static string[] additiveText = { "시럽 ", "설탕 ", "꿀 " };
    public static string[] lastOrderText = { "주시겠어요?", "줘봐.", "부탁 드려요..", ".", "주세요.", "부탁해~", "부탁할게." };
    public static string[] successOrderText = { "감사합니다!", "잘했어.", "수고하세요..", "감사합니다.",
                                            "감사해요.", "잘 마실게~", "다음에 또 올게." };
    public static string[] errorOrderText = { "죄송한데 제 음료가 아닌 것 같아요.", "이건 내가 주문한게 아니잖아!", "제가 주문한 음료가 맞나요..?", "이건 뭐야?",
                                            "이거 제가 시킨게 아닌데요.", "엥 이건 내가 주문한게 아닌데?", "주문한거 제대로 듣긴 한거야?" };
    public static int[] menuPrice = { 10, 15, 15, 20, 25, 30, 35, 30, 35 };

    public static bool isLobbyTutorial = false;
    public static bool isGameTutorial = false;
    public static int playerGold = 500; //플레이어 골드
    public static int playerDrinkUnlockNum = 3; //열린 음료 개수
    public static int playerAdditiveUnlockNum = 0;  //추가메뉴 언락 개수
    public static int maxOrderNum = 1;  //최대 주문을 받을 수 있는 개수
    public static int dayNumber = 1;
    
    // upgrad 확인
    public static int shopUpgradeNumber = 0;
    public static bool[] upgraded = new bool[100];

    // 사운드 파싱
    public static string[] carHornSounds = { "Sounds/Horns/CarHorn1", "Sounds/Horns/CarHorn2" };
    public static string[] getCoinSounds = { "Sounds/GetCoin", "Sounds/GetCoin2" };
    public static string[] hitSounds = { "Sounds/Hits/Hit1", "Sounds/Hits/Hit2" };
    public static string[] waterFillSounds = { "Sounds/WaterFill1", "Sounds/WaterFill2", "Sounds/WaterFill3" };
    public static string iceDropSound =  "Sounds/IceDrop";

    public static AudioClip hitClip1 = Resources.Load(hitSounds[0]) as AudioClip;
    public static AudioClip errorBeepClip;
    public static AudioClip buySound;
    public static AudioClip lobbyBGM;
    public static AudioClip inGameBGM;
    public static AudioClip btnClickSound;
    public static AudioClip chatSound;

    // 상점 루트
    public static Sprite[] shopImages = Resources.LoadAll<Sprite>("ShopImage");
    public static string[] shopInfoTexts = { "<color=yellow>[새로운 재료]</color> 우유", "<color=yellow>[새로운 메뉴]</color> 콜드브루", "<color=yellow>[새로운 업그레이드]</color> 더 많은 주문",
                        "<color=yellow>[새로운 재료]</color> 시럽","<color=yellow>[새로운 메뉴]</color> 블랙 티","<color=yellow>[새로운 업그레이드]</color> 메뉴 확인서", };
    public static string[] shopExTexts = { "라떼의 기본적인 재료입니다.", "차갑게 우려낸 커피", "이제부터 주문을 2개까지 받을 수 있습니다!",
                                            "단 걸 좋아하는 사람은 필수입니다.", "제일 대중적인 찻잎입니다.", "TAP키를 눌러 항상 메뉴가 뭔지 재 확인할 수 있습니다."};
    public static int[] shopCosts = { 100, 150, 200, 250, 250, 300 };

    public static string[] UpgradeName = {"", "우유", "콜드브루", "더 많은 주문", "시럽", "블랙 티", "메뉴 확인서" };

    public static int[] shopDrinkUpgradeNum     = { 2, 2, 0, 0, 2, 0};
    public static int[] shopAdditiveUpgradeNum  = { 0, 0, 0, 1, 0, 0};
    public static int[] shopUpgradeMaxOrderNum  = { 0, 0, 1, 0, 0, 0};

    public static string[,] recipeInfoTexts = { { "<size=35>물</size>(ICE/HOT)\n", "금액 : $10\n", "조합법 : 물 따르기\n", "갈증을 해소하는데 최고의 수단입니다.\n", "-추가 요소\n", ""},
                                                { "<size=35>아메리카노</size>(ICE/HOT)\n", "금액 : $15\n", "조합법 : 물 + 에스프레소 샷\n", "가장 기본적인 커피\n", "-추가 요소\n", "원두 선택, 시럽"},
                                                { "<size=35>녹차</size>(ICE/HOT)\n", "금액 : $15\n", "조합법 : 물 + 녹차 티백\n", "가장 기본적인 차\n", "-추가 요소\n", "시럽"},
                                                { "<size=35>카페라떼</size>(ICE/HOT)\n", "금액 : $20\n", "조합법 : 우유 + 에스프레소 샷\n", "부드러운 커피입니다.\n", "-추가 요소\n", "원두 선택, 시럽, 두유 변경"},
                                                { "<size=35>녹차라떼</size>(ICE/HOT)\n", "금액 : $25\n", "조합법 : 우유 + 녹차 티백\n", "부드러운 녹차맛을 느낄 수 있습니다.\n", "-추가 요소\n", "원두 선택, 시럽"},
                                                { "<size=35>콜드브루</size>(ICE/HOT)\n", "금액 : $30\n", "조합법 : 물 + 콜드브루 원액\n", "오랜시간 차갑게 우려낸 커피입니다.\n", "-추가 요소\n", "원두 선택, 시럽"},
                                                { "<size=35>콜드브루 라떼</size>(ICE/HOT)\n", "금액 : $35\n", "조합법 : 우유 + 콜드브루 원액\n", "부드러운 우유에 부드러운 원액이 합쳐지면..?\n", "-추가 요소\n", "원두 선택, 시럽"},
                                                { "<size=35>블랙티</size>(ICE/HOT)\n", "금액 : $30\n", "조합법 : 물 + 블랙티 티백\n", "일명 홍차라고 불립니다.\n", "-추가 요소\n", "시럽"},
                                                { "<size=35>블랙티 라떼</size>(ICE/HOT)\n", "금액 : $35\n", "조합법 : 우유 + 블랙티 티백\n", "홍차우유란 바로 이겁니다.\n", "-추가 요소\n", "시럽"},
                                                };

    public static int[,] recipeUpgradeNumber = { { 0, 0 }, /*{ "물" },*/
                                                 { 0, 0 }, /*{ "물", "에스프레소 샷", "아메리카노"},*/
                                                 { 0, 0 }, /*{ "물", "녹차 티백", "녹차"},*/
                                                 { (int)UpgradeState.Milk, 0 },                          /*{ "우유", "에스프레소 샷", "카페 라떼"},*/
                                                 { (int)UpgradeState.Milk, 0 },                          /*{ "우유", "녹차", "녹차 라떼"},*/
                                                 { 0, (int)UpgradeState.ColdBrue },                      /*{ "물", "콜드브루 원액", "콜드브루"},*/
                                                 { (int)UpgradeState.Milk, (int)UpgradeState.ColdBrue }, /*{ "우유", "콜드브루 원액", "콜드브루 라떼"},*/
                                                 { 0, (int)UpgradeState.BlackTea },                      /*{ "물", "블랙티", "블랙티"},*/
                                                 { (int)UpgradeState.Milk, (int)UpgradeState.BlackTea }, //{ "우유", "블랙티", "블랙티 라떼"},
                                                 };       
                                                          
}