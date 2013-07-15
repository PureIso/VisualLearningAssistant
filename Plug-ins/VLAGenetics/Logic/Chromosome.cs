
using System;
using System.Threading;

namespace VLAGenetics.Logic
{
    /// <summary>
    /// This is the set of properties which can be mutated and altered.
    /// </summary>
    public class Chromosome : IComparable<Chromosome>
    {
        private Encoding.Traits _encoding;
        private string _stringBinaryEncoding;

        public int Fitness;

        public  Encoding.Traits Encoding
        {
            get { return _encoding; }
        }

        public string StringBinaryEncoding
        {
            get {return _stringBinaryEncoding; }
            set
            {
                if (_stringBinaryEncoding == value) return;
                _encoding.Dimples = (Encoding.Dimples)(int.Parse(value[0].ToString()));
                _encoding.Handedness = (Encoding.Handedness)(int.Parse(value[1].ToString()));
                _encoding.Freckles = (Encoding.Freckles)(int.Parse(value[2].ToString()));
                _encoding.Hair = (Encoding.Hair)(int.Parse(value[3].ToString()));
                _encoding.Height = (Encoding.Height)(int.Parse(value[4].ToString()));
                _encoding.Eyes = (Encoding.Eyes)(int.Parse(value[5].ToString()));
                _encoding.BloodType = (Encoding.BloodType)(int.Parse(value[6].ToString()));
                _encoding.AbilityOne = (Encoding.AbilityOne)(int.Parse(value[7].ToString()));
                _encoding.AbilityTwo = (Encoding.AbilityTwo)(int.Parse(value[8].ToString()));
                _encoding.AbilityThree = (Encoding.AbilityThree)(int.Parse(value[9].ToString()));
                _stringBinaryEncoding = value;
            }
        }

        public string StringTraitEncoding
        {
            get { return StringEncoder(_encoding); }
        }

        public Chromosome()
        {
            //Randomize the chromosomes
            Random randomValues = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int32.MaxValue));

            _encoding.Dimples = (Encoding.Dimples)(randomValues.Next() % 2);
            _encoding.Handedness = (Encoding.Handedness)(randomValues.Next() % 2);
            _encoding.Freckles = (Encoding.Freckles)(randomValues.Next() % 2);
            _encoding.Hair = (Encoding.Hair)(randomValues.Next() % 2);
            _encoding.Height = (Encoding.Height)(randomValues.Next() % 2);
            _encoding.Eyes = (Encoding.Eyes)(randomValues.Next() % 2);
            _encoding.BloodType = (Encoding.BloodType)(randomValues.Next() % 2);
            _encoding.AbilityOne = (Encoding.AbilityOne)(randomValues.Next() % 2);
            _encoding.AbilityTwo = (Encoding.AbilityTwo)(randomValues.Next() % 2);
            _encoding.AbilityThree = (Encoding.AbilityThree)(randomValues.Next() % 2);

            _stringBinaryEncoding = StringBinaryEncoder(_encoding);
            Thread.Sleep(1);
        }

        public Chromosome(Encoding.Dimples dimples,
            Encoding.Handedness handedness,
            Encoding.Freckles freckles,
            Encoding.Hair hair,
            Encoding.Height height,
            Encoding.Eyes eye,
            Encoding.BloodType bloodType,
            Encoding.AbilityOne abilityOne,
            Encoding.AbilityTwo abilityTwo,
           Encoding.AbilityThree abilityThree)
        {
            _encoding.Dimples = dimples;
            _encoding.Handedness = handedness;
            _encoding.Freckles = freckles;
            _encoding.Hair = hair;
            _encoding.Height = height;
            _encoding.Eyes = eye;
            _encoding.BloodType = bloodType;
            _encoding.AbilityOne = abilityOne;
            _encoding.AbilityTwo = abilityTwo;
            _encoding.AbilityThree = abilityThree;

            _stringBinaryEncoding = StringBinaryEncoder(_encoding);
            Thread.Sleep(1);
        }

        private static string StringEncoder(Encoding.Traits encoding)
        {
           return  encoding.Dimples + "-" +
               encoding.Handedness + "-" +
               encoding.Freckles + "-" +
               encoding.Hair + "-" +
               encoding.Height + "-" +
               encoding.Eyes + "-" +
               encoding.BloodType + "-" +
               encoding.AbilityOne + "-" +
               encoding.AbilityTwo + "-" +
               encoding.AbilityThree;
        }

        private static string StringBinaryEncoder(Encoding.Traits encoding)
        {
            return (int)encoding.Dimples +""+
                (int)encoding.Handedness +
                (int)encoding.Freckles +
                (int)encoding.Hair+
                (int)encoding.Height +
                (int)encoding.Eyes +
                (int)encoding.BloodType +
                (int)encoding.AbilityOne +
                (int)encoding.AbilityTwo +
                (int)encoding.AbilityThree;
        }


        public int CompareTo(Chromosome other)
        {
            return Fitness.CompareTo(other.Fitness);
        }
    }
}
