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
        public AdditiveItem additiveItem = AdditiveItem.None;   //�ϴ� �÷� Ƚ���� ����
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

    public static string[] firstOrderText = { "�ȳ��ϼ���! ", "�̺� ", "��.. ", "", "", "�ȳ� ", "�� �Դ� " };
    public static string[] icehotOrderText = { "������ ", "���̽� ", ""};
    public static string[] menuOrderText = {"�� ���� ", "�Ƹ޸�ī�� ���� ", "���� ���� ", "ī�� �� ", "���� �� ", "�ݵ��� ���� ", "�ݵ��� �� ", "��Ƽ ", "��Ƽ �� " };
    public static string[] menuNameText = {"��", "�Ƹ޸�ī��", "����", "ī�� ��", "���� ��", "�ݵ���",  "�ݵ��� ��", "��Ƽ", "��Ƽ ��" };
    public static string[] iceOrderText = {"���� ���� ", "���� ���� "," ","���� ���� " };
    public static string[] iceNameText = {"���� ����", "���� ����","���� ������","���� ����" };
    public static string[] additiveText = { "�÷� ", "���� ", "�� " };
    public static string[] lastOrderText = { "�ֽðھ��?", "���.", "��Ź �����..", ".", "�ּ���.", "��Ź��~", "��Ź�Ұ�." };
    public static string[] successOrderText = { "�����մϴ�!", "���߾�.", "�����ϼ���..", "�����մϴ�.",
                                            "�����ؿ�.", "�� ���ǰ�~", "������ �� �ð�." };
    public static string[] errorOrderText = { "�˼��ѵ� �� ���ᰡ �ƴ� �� ���ƿ�.", "�̰� ���� �ֹ��Ѱ� �ƴ��ݾ�!", "���� �ֹ��� ���ᰡ �³���..?", "�̰� ����?",
                                            "�̰� ���� ��Ų�� �ƴѵ���.", "�� �̰� ���� �ֹ��Ѱ� �ƴѵ�?", "�ֹ��Ѱ� ����� ��� �Ѱž�?" };
    public static int[] menuPrice = { 10, 15, 15, 20, 25, 30, 35, 30, 35 };

    public static bool isLobbyTutorial = false;
    public static bool isGameTutorial = false;
    public static int playerGold = 500; //�÷��̾� ���
    public static int playerDrinkUnlockNum = 3; //���� ���� ����
    public static int playerAdditiveUnlockNum = 0;  //�߰��޴� ��� ����
    public static int maxOrderNum = 1;  //�ִ� �ֹ��� ���� �� �ִ� ����
    public static int dayNumber = 1;
    
    // upgrad Ȯ��
    public static int shopUpgradeNumber = 0;
    public static bool[] upgraded = new bool[100];

    // ���� �Ľ�
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

    // ���� ��Ʈ
    public static Sprite[] shopImages = Resources.LoadAll<Sprite>("ShopImage");
    public static string[] shopInfoTexts = { "<color=yellow>[���ο� ���]</color> ����", "<color=yellow>[���ο� �޴�]</color> �ݵ���", "<color=yellow>[���ο� ���׷��̵�]</color> �� ���� �ֹ�",
                        "<color=yellow>[���ο� ���]</color> �÷�","<color=yellow>[���ο� �޴�]</color> �� Ƽ","<color=yellow>[���ο� ���׷��̵�]</color> �޴� Ȯ�μ�", };
    public static string[] shopExTexts = { "���� �⺻���� ����Դϴ�.", "������ ����� Ŀ��", "�������� �ֹ��� 2������ ���� �� �ֽ��ϴ�!",
                                            "�� �� �����ϴ� ����� �ʼ��Դϴ�.", "���� �������� �����Դϴ�.", "TAPŰ�� ���� �׻� �޴��� ���� �� Ȯ���� �� �ֽ��ϴ�."};
    public static int[] shopCosts = { 100, 150, 200, 250, 250, 300 };

    public static string[] UpgradeName = {"", "����", "�ݵ���", "�� ���� �ֹ�", "�÷�", "�� Ƽ", "�޴� Ȯ�μ�" };

    public static int[] shopDrinkUpgradeNum     = { 2, 2, 0, 0, 2, 0};
    public static int[] shopAdditiveUpgradeNum  = { 0, 0, 0, 1, 0, 0};
    public static int[] shopUpgradeMaxOrderNum  = { 0, 0, 1, 0, 0, 0};

    public static string[,] recipeInfoTexts = { { "<size=35>��</size>(ICE/HOT)\n", "�ݾ� : $10\n", "���չ� : �� ������\n", "������ �ؼ��ϴµ� �ְ��� �����Դϴ�.\n", "-�߰� ���\n", ""},
                                                { "<size=35>�Ƹ޸�ī��</size>(ICE/HOT)\n", "�ݾ� : $15\n", "���չ� : �� + ���������� ��\n", "���� �⺻���� Ŀ��\n", "-�߰� ���\n", "���� ����, �÷�"},
                                                { "<size=35>����</size>(ICE/HOT)\n", "�ݾ� : $15\n", "���չ� : �� + ���� Ƽ��\n", "���� �⺻���� ��\n", "-�߰� ���\n", "�÷�"},
                                                { "<size=35>ī���</size>(ICE/HOT)\n", "�ݾ� : $20\n", "���չ� : ���� + ���������� ��\n", "�ε巯�� Ŀ���Դϴ�.\n", "-�߰� ���\n", "���� ����, �÷�, ���� ����"},
                                                { "<size=35>������</size>(ICE/HOT)\n", "�ݾ� : $25\n", "���չ� : ���� + ���� Ƽ��\n", "�ε巯�� �������� ���� �� �ֽ��ϴ�.\n", "-�߰� ���\n", "���� ����, �÷�"},
                                                { "<size=35>�ݵ���</size>(ICE/HOT)\n", "�ݾ� : $30\n", "���չ� : �� + �ݵ��� ����\n", "�����ð� ������ ����� Ŀ���Դϴ�.\n", "-�߰� ���\n", "���� ����, �÷�"},
                                                { "<size=35>�ݵ��� ��</size>(ICE/HOT)\n", "�ݾ� : $35\n", "���չ� : ���� + �ݵ��� ����\n", "�ε巯�� ������ �ε巯�� ������ ��������..?\n", "-�߰� ���\n", "���� ����, �÷�"},
                                                { "<size=35>��Ƽ</size>(ICE/HOT)\n", "�ݾ� : $30\n", "���չ� : �� + ��Ƽ Ƽ��\n", "�ϸ� ȫ����� �Ҹ��ϴ�.\n", "-�߰� ���\n", "�÷�"},
                                                { "<size=35>��Ƽ ��</size>(ICE/HOT)\n", "�ݾ� : $35\n", "���չ� : ���� + ��Ƽ Ƽ��\n", "ȫ�������� �ٷ� �̴̰ϴ�.\n", "-�߰� ���\n", "�÷�"},
                                                };

    public static int[,] recipeUpgradeNumber = { { 0, 0 }, /*{ "��" },*/
                                                 { 0, 0 }, /*{ "��", "���������� ��", "�Ƹ޸�ī��"},*/
                                                 { 0, 0 }, /*{ "��", "���� Ƽ��", "����"},*/
                                                 { (int)UpgradeState.Milk, 0 },                          /*{ "����", "���������� ��", "ī�� ��"},*/
                                                 { (int)UpgradeState.Milk, 0 },                          /*{ "����", "����", "���� ��"},*/
                                                 { 0, (int)UpgradeState.ColdBrue },                      /*{ "��", "�ݵ��� ����", "�ݵ���"},*/
                                                 { (int)UpgradeState.Milk, (int)UpgradeState.ColdBrue }, /*{ "����", "�ݵ��� ����", "�ݵ��� ��"},*/
                                                 { 0, (int)UpgradeState.BlackTea },                      /*{ "��", "��Ƽ", "��Ƽ"},*/
                                                 { (int)UpgradeState.Milk, (int)UpgradeState.BlackTea }, //{ "����", "��Ƽ", "��Ƽ ��"},
                                                 };       
                                                          
}