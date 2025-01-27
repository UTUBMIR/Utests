```cs
namespace Example_ {
    internal class Program {
        static object test1() {
            Console.WriteLine("2 * 6");
            return 2 * 6;
        }

        static void Main(string[] args) {
            UTests uTests = new UTests();

            uTests.tests?.Add(test1);

            uTests.PassedPrint([12]);
        }
    }
}

```
