using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
    public class MonsterInABox_Item : Item
    {
        public string mName;
		
		public Server.Mobiles.AIType _AI { get; set; }
		public int _Str { get; set; }
		private int _ItemValue;
		private int _BodyValue;
		private int _ValueIndex;
		
		[CommandProperty(AccessLevel.GameMaster)]
        public int ValueIndex { 
			get { return _ValueIndex; } set { _ValueIndex = value; } 
		}
		
		[CommandProperty(AccessLevel.GameMaster)]
        public int ItemValue
        { 
			get {
               if ( ItemValueDictionary.ItemNumber.Length > 0) {
                    if (_ValueIndex >= ItemValueDictionary.ItemNumber.Length) {
                        _ValueIndex = 0;
                    }
                    else if (_ValueIndex < 0) {
                        _ValueIndex = ItemValueDictionary.ItemNumber.Length - 1;
                    }

                    return ItemValueDictionary.ItemNumber[ _ValueIndex ];
				}
				return _ItemValue;
            }
            set {  _ItemValue = value; }
        }
        
		[CommandProperty(AccessLevel.GameMaster)]
        public int BodyValue
        { 
			get {
               if ( ItemValueDictionary.BodyNumber.Length > 0) {
                    if (_ValueIndex >= ItemValueDictionary.BodyNumber.Length) {
                        _ValueIndex = 0;
                    }
                    else if (_ValueIndex < 0) {
                        _ValueIndex = ItemValueDictionary.BodyNumber.Length - 1;
                    }

                    return ItemValueDictionary.BodyNumber[ _ValueIndex ];
				}
				return _BodyValue;
            }
            set {  _BodyValue = value; }
        }
        
		
		[Constructable]
        public MonsterInABox_Item() : base (0xE80)
        {
            this.Weight = 1.0;
        }
		
		public MonsterInABox_Item(Serial serial) : base(serial) {}
       
        public override void OnDoubleClick(Mobile from)
        {
			if (from.IsStaff() || from.InRange(GetWorldLocation(), 2)) {
                from.SendGump( new Miab_Gump( from, this ) );
			}
            else {
                from.SendLocalizedMessage(500446); // That is too far away.
            }
        }

       

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
           
            writer.Write( mName );
			writer.Write( (int)_AI );
			writer.Write( (int)_Str );
			writer.Write( (int)_ItemValue );
			writer.Write( (int)_BodyValue );
			writer.Write( (int)_ValueIndex );
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
           
            mName = reader.ReadString();
			_AI = (AIType)reader.ReadInt();
			_Str = reader.ReadInt();
			_ItemValue = reader.ReadInt();
			_BodyValue = reader.ReadInt();
			_ValueIndex = reader.ReadInt();
        }
    }
}