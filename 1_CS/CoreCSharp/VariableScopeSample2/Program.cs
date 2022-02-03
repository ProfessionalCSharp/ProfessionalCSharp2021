int j = 20;
for (int i = 0; i < 10; i++)
{
    // int j = 30; // Can't do this — j is still in scope - uncomment to get the compilation error
    Console.WriteLine(j + i);
}
