int? n1 = null;
if (n1.HasValue)
{
    int n2 = n1.Value;
}
int n3 = 42;
int? n4 = n3;

Nullable<int> n5 = null;
int n6 = n5.GetValueOrDefault();