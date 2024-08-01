using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {

            //Starting example list of chips
            var chipList = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
            };

            //Call function using the chip list to gather sequence result
            var sequenceResult = CheckSequenceOrder(chipList, Color.Blue, Color.Green);

            //Check if result is null to determine if correct sequence was found.
            if (sequenceResult != null)
            {
                Console.WriteLine("Correct sequence found:");
                //Loop through results and display to user.
                foreach (var i in sequenceResult)
                {
                    Console.WriteLine($"[{i.StartColor}, {i.EndColor}]");
                }
            }
            else
            {
                //If an invalid sequence, show error.
                Console.WriteLine(Constants.ErrorMessage);
            }


        }


        //Method for providing the list of sequenced results.
        public static List<ColorChip> CheckSequenceOrder(List<ColorChip> chipList, Color startColor, Color endColor)
        {
            //Initialize the sequence of ColorChip.
            List<ColorChip> sequence = new List<ColorChip>();

            //Invoke FindSequenceOrder method to determine if a valid sequence was found.
            bool foundSequence = FindSequenceOrder(chipList, startColor, endColor, sequence);

            //Check whether or not the method returns a valid result.
            return (foundSequence) ? sequence : null;
        }

        //Method to determine a valid sequence order of chips.
        public static bool FindSequenceOrder(List<ColorChip> chipList, Color currentColor, Color endColor, List<ColorChip> sequence)
        {
            //Check to determine if the sequence is valid or not.
            if (currentColor == endColor)
            {
                return true;
            }

            //Loop through all chips to see if it can be placed next in the sequence.
            for (int i = 0; i < chipList.Count; i++)
            {
                //Create object for each item in the list, starting at inital position 0.
                ColorChip chip = chipList[i];

                //Check if the start color matches the current color.
                if (chip.StartColor == currentColor)
                {
                    //If it matches, add the chip to the sequence
                    sequence.Add(chip);
                    //Remove that chip from the list.
                    chipList.RemoveAt(i);

                    //Check the order again
                    if (FindSequenceOrder(chipList, chip.EndColor, endColor, sequence))
                    {
                        //If a valid sequence, proceed as true.
                        return true;
                    }

                    //If it wasn't a valid sequence, remove the chip and put it back in the list.
                    chipList.Insert(i, chip);
                    sequence.RemoveAt(sequence.Count - 1);
                }
                //Check if the end color matches the current color.
                else if (chip.EndColor == currentColor)
                {
                    //If it matches, add the chip to the sequence
                    sequence.Add(chip);
                    //Remove that chip from the list.
                    chipList.RemoveAt(i);

                    //Check the order again
                    if (FindSequenceOrder(chipList, chip.StartColor, endColor, sequence))
                    {
                        //If a valid sequence, proceed as true.
                        return true;
                    }

                    //If it wasn't a valid sequence, remove the chip and put it back in the list.
                    chipList.Insert(i, chip);
                    sequence.RemoveAt(sequence.Count - 1);
                }
            }
            // If no valid sequence is found, return false
            return false;
        }
    }
}
