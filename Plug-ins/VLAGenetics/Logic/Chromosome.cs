
using System;
using System.Threading;

namespace VLAGenetics.Logic
{
    /// <summary>
    /// This is the set of properties which can be mutated and altered.
    /// </summary>
    public class Chromosome : IComparable<Chromosome>
    {
        private readonly Encoding.Traits _encoding;

        public  Encoding.Traits Encoding
        {
            get { return _encoding; }
        }

        public int Fitness;
        public string StringEncoding;
        public string StringEncodingOneAndZeros;

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

            StringEncoding = StringEncoder(_encoding);
            StringEncodingOneAndZeros = StringEncoderOneAndZeros(_encoding);
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

            StringEncoding = StringEncoder(_encoding);
            StringEncodingOneAndZeros = StringEncoderOneAndZeros(_encoding);
            Thread.Sleep(1);
        }

        private string StringEncoder(Encoding.Traits encoding)
        {
           return StringEncoding = encoding.Dimples + "-" +
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

        private string StringEncoderOneAndZeros(Encoding.Traits encoding)
        {
            return StringEncoding = (int)encoding.Dimples +""+
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
