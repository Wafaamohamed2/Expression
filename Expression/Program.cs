using System;
using System.Linq.Expressions;

internal class Program
{
    private static void Main(string[] args)
    {
        //onvert the  Func type delegate into an Expression
        //by wrapping Func delegate with Expresson

        Expression<  Func<Student, bool> > isTeengerExp = s => 
        s.Age > 12 && s.Age < 18;


        //The compiler will translate the above expression
        //into the following expression tree:


        ParameterExpression pe = Expression.Parameter(typeof(Student), "s");
        MemberExpression me = Expression.Property(pe, "Age");

      

        //  combined expression with And
        BinaryExpression combined = Expression.AndAlso(
            Expression.GreaterThanOrEqual(Expression.Constant(12, typeof(int)), Expression.Property(pe, "Age")),
            Expression.LessThanOrEqual(Expression.Constant(18, typeof(int)), Expression.Property(pe, "Age"))
        );





        var ExpressionTree = Expression.Lambda<Func<Student, bool>>(combined, new[] { pe });
        Console.WriteLine("Expression Tree: {0}", ExpressionTree);

        Console.WriteLine("Expression Tree Body Combined: {0}", combined);

        Console.WriteLine("Number of Parameters in Expression Tree: {0}",
                                        ExpressionTree.Parameters.Count);

        Console.WriteLine("Parameters in Expression Tree: {0}", ExpressionTree.Parameters[0]);









        //Invoke an Expression using the Compile() method.
        //Compile() returns delegateof Func 

        Func<Student , bool> istneeger = isTeengerExp.Compile();
        bool result = istneeger(new Student() { Id =1 , Name = "Ali" , Age =16});
        Console.WriteLine("================");
        Console.WriteLine(result);

    }


    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}