namespace Learning_c_ {
    internal class UTests {
        internal List<Func<object>>? tests;

        public UTests() {
            tests = new List<Func<object>>();
        }

        internal static string absoluteToString(object abs) {
            if (abs is Array array) {
                return "[" + string.Join(", ", array.Cast<object>()) + "]";
            }
            return abs.ToString()??"NONE";
        }

        internal static bool absoluteEquals(object absA, object absB) {
            if (absA is Array arrA && absB is Array arrB) {
                return arrA.Cast<object>().SequenceEqual(arrB.Cast<object>());
            }
            return Equals(absA, absB);
        }

        internal object[]? UnitTest() {
            if (tests == null) return null;

            object[] result = new object[tests.Count];
            for (int i = 0; i < tests.Count; ++i) {
                result[i] = tests[i]();
            }
            return result;
        }

        internal object[]? UnitTestPrint() {
            if (tests == null) return null;

            object[] result = new object[tests.Count];

            Console.WriteLine("Starting UnitTests...\n");
            for (int i = 0; i < tests.Count; ++i) {
                Console.WriteLine($"{tests[i].Method.Name} {{");
                result[i] = tests[i]();
                Console.WriteLine($"}} returned: {absoluteToString(result[i])}\n");
            }
            Console.WriteLine("UnitTests finished\n");

            return result;
        }

        internal bool? UnitTest(object[] right) {
            object[]? tested = UnitTest();
            if (tested == null) return null;

            return tested.SequenceEqual(right);
        }

        internal bool[]? Passed(object[] right) {
            object[]? tested = UnitTest();
            if (tested == null || tested.Length != right.Length) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\aError: Wrong length of \"right\" parameters!");
                Console.ResetColor();
                return null;
            }

            return tested.Zip(right, absoluteEquals).ToArray();
        }

        internal bool[]? PassedPrint(object[] right) {
            if (tests == null || right.Length != tests.Count) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\aError: Wrong length of \"right\" parameters!");
                Console.ResetColor();
                return null;
            }

            bool[] passed = new bool[tests.Count];
            int passedCounter = 0;

            Console.WriteLine("Starting UnitTests...\n");
            for (int i = 0; i < tests.Count; ++i) {
                Console.WriteLine($"{tests[i].Method.Name} {{");
                object tested = tests[i]();
                passed[i] = absoluteEquals(tested, right[i]);

                Console.Write($"}} returned: \"{absoluteToString(tested)}\" of type: {tested.GetType()}, ");
                Console.ForegroundColor = passed[i] ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(passed[i] ? "PASSED" : "FAILED");
                Console.ResetColor();

                Console.WriteLine($", pass value: \"{absoluteToString(right[i])}\" of type: {right[i].GetType()}.\n");

                if (passed[i]) ++passedCounter;
            }

            Console.WriteLine($"UnitTests finished.");

            if (tests.Count > 0) {
                double percent = (passed.Length != 0) ? ((double)passedCounter / passed.Length) * 100 : 0;
                Console.Write($"With: {passedCounter} passed and {passed.Length - passedCounter} failed. {percent:N2}%. Most is ");

                Console.ForegroundColor = percent > 50 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(percent > 50 ? "PASSED.\n" : "FAILED.\n");
                Console.ResetColor();
            }

            return passed;
        }
    }
}