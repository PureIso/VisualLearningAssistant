
namespace VLAGenetics.Logic
{
    /// <summary>
    /// This are the Traits Encoding insteaded of 0s and 1s
    /// </summary>
    public static class Encoding
    {
        public enum Dimples
        {
            Ignore = -1,
            Dimples = 1,
            NoDimples = 0
        }
        public enum Handedness
        {
            Ignore = -1,
            RightHanded = 1,
            LeftHanded = 0,
        }
        public enum Freckles
        {
            Ignore = -1,
            Freckles = 1,
            NoFreckles = 0,
        }
        public enum Hair
        {
            Ignore = -1,
            NaturallyCurlyHair = 1,
            NaturallyStraightHair = 0,
        }
        public enum Height
        {
            Ignore = -1,
            Tall = 1,
            Short = 0,
        }
        public enum Eyes
        {
            Ignore = -1,
            Hazel = 1,
            Blue = 0,
        }
        public enum BloodType
        {
            Ignore = -1,
            A = 1,
            O = 0,
        }
        public enum AbilityOne
        {
            Ignore = -1,
            Speed = 1,
            Flight = 0,
        }
        public enum AbilityTwo
        {
            Ignore = -1,
            Intelligence = 1,
            Strength = 0,
        }
        public enum AbilityThree
        {
            Ignore = -1,
            ThunderStrike = 1,
            FireBall = 0,
        }

        public static int EncodingCount = 10;

        public struct Traits
        {
            public Dimples Dimples;
            public Handedness Handedness;
            public Freckles Freckles;
            public Hair Hair;
            public Height Height;
            public Eyes Eyes;
            public BloodType BloodType;
            public AbilityOne AbilityOne;
            public AbilityTwo AbilityTwo;
            public AbilityThree AbilityThree;
        }
    }
}
