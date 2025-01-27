using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_c_ {
    internal class UTests {
        internal List<Func<object>>? tests;

        public UTests() {
            tests = new List<Func<object>>();
        }

        internal object[]? UnitTest() {
            if (tests == null) {
                return null;
            }
            object[] result = new object[tests.Count];

            for (int i = 0; i < tests.Count; ++i) {
                result[i] = tests[i]();
            }

            return result;
        }

        internal object[]? UnitTestPrint() {
            if (tests == null) {
                return null;
            }
            object[] result = new object[tests.Count];

            Console.WriteLine("Starting UnitTests...\n");
            for (int i = 0; i < tests.Count; ++i) {
                Console.WriteLine($"{tests[i].Method.Name} {{");
                result[i] = tests[i]();
                Console.WriteLine($"}} returned: {result[i]}\n");
            }
            Console.WriteLine("UnitTests finished\n");

            return result;
        }

        internal bool? UnitTest(object[] right) {
            return UnitTest() == right;
        }

        internal bool[]? Passed(object[] right) {
            object[]? tested = UnitTest();
            if (tested == null || right.Length != tested?.Length) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\aError: Wrong length of \"right\" parameters!");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }

            bool[] passed = new bool[tests.Count];

            for (int i = 0; i < tested.Length; ++i) {
                passed[i] = tested[i] == right[i];
            }

            return passed;
        }

        internal bool[]? PassedPrint(object[] right) {
            object[] tested = new object[tests.Count];

            if (tests == null || right.Length != tested.Length) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\aError: Wrong length of \"right\" parameters!");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
            bool[] passed = new bool[tests.Count];
            int passedCounter = 0;

            Console.WriteLine("Starting UnitTests...\n");
            for (int i = 0; i < tests.Count; ++i) {
                Console.WriteLine($"{tests[i].Method.Name} {{");
                tested[i] = tests[i]();
                passed[i] = tested[i].Equals(right[i]);
                Console.Write($"}} returned: \"{tested[i]}\" of type: {tested[i].GetType()}, ");

                Console.ForegroundColor = passed[i] ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write($"{ (passed[i] ? "PASSED" : "FAILED")}");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($", pass value: \"{right[i]}\" of type: {right[i].GetType()}.\n");

                passedCounter += passed[i] ? 1 : 0;
            }
            Console.WriteLine($"UnitTests finished.");

            if (tests.Count != 0) {
                double percent = (passed.Length != 0 ? ((double)passedCounter / passed.Length) : 0);
                Console.Write($"\bwith: {passedCounter} passed and {passed.Length - passedCounter} failed. {(percent * 100):N2}%. Most is ");
                
                Console.ForegroundColor = percent > 0.5 ? ConsoleColor.Green : ConsoleColor.Red;

                if (percent > 0.5) {
                    Console.WriteLine("PASSED.\n");
                }
                else {
                    Console.WriteLine("FAILED.\n");
                }

                Console.ForegroundColor = ConsoleColor.White;
            }

            return passed;
        }
    }
}
