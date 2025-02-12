using System;

namespace Assignment_3 {
    public class Class3 {

            static void Main(string[] args) {
                int length = 0;
                do {
                    Console.Write("Until which number should the prime numbers be calculated?");
                } while (!int.TryParse(Console.ReadLine(), out length));
                int[] numbers = new int[length + 1];

                // computing
                GenerateArray(numbers);
                SieveOfEra(numbers);
                Console.WriteLine($"Prime numbers from 2 - {length}");
                PrintArray(numbers);
            }

            // fills the array with values corresponding to their index
            private static void GenerateArray(int[] nums) {
                for (int i = 0; i < nums.Length; i++) {
                    nums[i] = i;
                }
            }

            // prints the passed array if value not = 0
            private static void PrintArray(int[] nums) {
                for (int i = 2; i < nums.Length; i++) {
                    if (nums[i] != 0) Console.Write($"{nums[i]} ");
                }
            }

            // replaces non prime numbers with 0
            private static void SieveOfEra(int[] nums) {
                // increment p to the next non marked larger number
                for (int p = 2; p < nums.Length; p++) {
                    if (nums[p] == 0) continue;
                    // purge non primes
                    for (int i = 2; (i * p) < nums.Length; i++) {
                        nums[(i * p)] = 0;
                    }
                }
            }
    }
}

