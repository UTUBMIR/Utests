# Simple example
```cs
namespace Example {
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

after starting this code you will see this:
![image](https://github.com/user-attachments/assets/9a168c45-50d2-477f-ba3d-2d6508aec634)

- [x] Support any return type in test functions
- [ ] Support any parameters it test function
- [ ] Print formatting
