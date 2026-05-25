using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Project
{
	/// <summary>
	/// Description of Pol.
	/// </summary>
	public class Pol
    {
        public Dictionary<float,float> D;
        public Pol Z;
        public Pol()
        {
            this.D = new Dictionary<float,float>();
        }
        public Pol(Pol A)
        {
            this.D = new Dictionary<float,float>(A.D);
            if(A.Z != null)
            {
                this.Z = new Pol();
                this.Z.D = new Dictionary<float,float>(A.Z.D);
            }
        }
        public static Pol Add(Pol A, Pol B)
        {
            var C = new Pol();
            var A1 = new Pol(A);
            var B1 = new Pol(B);
            if(A.Z != null)
            {
                if(B.Z == null)
                {
                    C.Z = new Pol(A1.Z);
                    B1 = Mult(B1,A1.Z);
                }
                else
                {
                    A1 = Mult(A1,B1.Z);
                    B1 = Mult(B1,A1.Z);
                    C.Z = new Pol(Mult(A1.Z,B1.Z));
                }
            }
            else
            {
                if(B.Z != null)
                {
                    C.Z = new Pol(B1.Z);
                    A1 = Mult(A1,B1.Z);
                }
            }
            foreach(KeyValuePair<float,float> kvp in A1.D)
            {
                C.D.Add(kvp.Key,kvp.Value);
            }
            foreach(KeyValuePair<float,float> kvp in B1.D)
            {
                if(C.D.ContainsKey(kvp.Key))
                {
                    C.D[kvp.Key] += kvp.Value;
                    if(C.D[kvp.Key] == 0)
                    {
                        C.D.Remove(kvp.Key);
                    }
                }
                else
                {
                    C.D.Add(kvp.Key,kvp.Value);
                }
            }
            return C;
        }
        public static Pol Subtract(Pol A, Pol B)
        {
            var C = new Pol();
            var A1 = new Pol(A);
            var B1 = new Pol(B);
            if(A.Z != null)
            {
                if(B.Z == null)
                {
                    C.Z = new Pol(A1.Z);
                    B1 = Mult(B1,A1.Z);
                }
                else
                {
                    A1 = Mult(A1,B1.Z);
                    B1 = Mult(B1,A1.Z);
                    C.Z = new Pol(Mult(A1.Z,B1.Z));
                }
            }
            else
            {
                if(B.Z != null)
                {
                    C.Z = new Pol(B1.Z);
                    A1 = Mult(A1,B1.Z);
                }
            }
            foreach(KeyValuePair<float,float> kvp in A1.D)
            {
                C.D.Add(kvp.Key,kvp.Value);
            }
            foreach(KeyValuePair<float,float> kvp in B1.D)
            {
                if(C.D.ContainsKey(kvp.Key))
                {
                    C.D[kvp.Key] -= kvp.Value;
                    if(C.D[kvp.Key] == 0)
                    {
                        C.D.Remove(kvp.Key);
                    }
                }
                else
                {
                    C.D.Add(kvp.Key,-kvp.Value);
                }
            }
            return C;
        }
        public static Pol Mult(Pol A, Pol B)
        {
            var C = new Pol();
            foreach(KeyValuePair<float,float> kvp1 in A.D)
            {
                foreach(KeyValuePair<float,float> kvp2 in B.D)
                {
                    if(C.D.ContainsKey(kvp1.Key+kvp2.Key))
                    {
                        C.D[kvp1.Key+kvp2.Key] += kvp1.Value*kvp2.Value;
                    }
                    else
                    {
                        C.D.Add(kvp1.Key+kvp2.Key,kvp1.Value*kvp2.Value);
                    }
                }
            }
            if(A.Z != null || B.Z != null)
            {
                if(A.Z == null)
                {
                    C.Z = new Pol(B.Z);
                }
                else if(B.Z == null)
                {
                    C.Z = new Pol(A.Z);
                }
                else
                {
                    C.Z = new Pol();
                    foreach(KeyValuePair<float,float> kvp1 in A.Z.D)
                    {
                        foreach(KeyValuePair<float,float> kvp2 in B.Z.D)
                        {
                            if(C.Z.D.ContainsKey(kvp1.Key+kvp2.Key))
                            {
                                C.Z.D[kvp1.Key+kvp2.Key] += kvp1.Value*kvp2.Value;
                            }
                            else
                            {
                                C.Z.D.Add(kvp1.Key+kvp2.Key,kvp1.Value*kvp2.Value);
                            }
                        }
                    }
                }
            }
            return C;
        }
        public static Pol Polify(string s)
        {
            var S = new Stack<Pol>();
            var C = new Stack<char>();
            var P = new Pol();
            char zn = '+';
            for(int i = 0; i<s.Length; i++)
            {
                switch(s[i])
                {
                    case '+': zn = '+'; break;
                    case '-': zn = '-'; break;
                    case '*': zn = '*'; break;
                    case '/': zn = '/'; break;
                }
                if(Conv(s[i])>-1 && Conv(s[i])<10)
                {
                    var A = new StringBuilder();
                    while(i<s.Length && Conv(s[i])>-1 && Conv(s[i])<10)
                    {
                        A.Append(s[i]);
                        i++;
                    }
                    float k = float.Parse(A.ToString());
                    if(i<s.Length)
                    {
                        if(s[i] == 'x')
                        {
                            i++;
                            if(i == s.Length || s[i] != '^')
                            {
                                i--;
                                var P1 = new Pol();
                                P1.D.Add(1,k);
                                S.Push(P1);
                                C.Push(zn);
                            }
                            else
                            {
                                i++;
                                A.Clear();
                                while(i<s.Length && Conv(s[i])>-1 && Conv(s[i])<10)
                                {
                                    A.Append(s[i]);
                                    i++;
                                }
                                i--;
                                int st = int.Parse(A.ToString());
                                var P1 = new Pol();
                                P1.D.Add(st,k);
                                S.Push(P1);
                                C.Push(zn);
                            }
                        }
                        else
                        {
                            i--;
                            var P1 = new Pol();
                            P1.D.Add(0,k);
                            S.Push(P1);
                            C.Push(zn);
                        }
                    }
                    else
                    {
                        var P1 = new Pol();
                        P1.D.Add(0,k);
                        S.Push(P1);
                        C.Push(zn);
                    }
                }
                else if(s[i] == 'x')
                {
                    i++;
                    if(i == s.Length || s[i] != '^')
                    {
                        i--;
                        var P1 = new Pol();
                        P1.D.Add(1,1);
                        S.Push(P1);
                        C.Push(zn);
                    }
                    else
                    {
                        i++;
                        var A = new StringBuilder();
                        while(i<s.Length && Conv(s[i])>-1 && Conv(s[i])<10)
                        {
                            A.Append(s[i]);
                            i++;
                        }
                        i--;
                        int st = int.Parse(A.ToString());
                        var P1 = new Pol();
                        P1.D.Add(st,1);
                        S.Push(P1);
                        C.Push(zn);
                    }
                }
                else if(s[i] == '(')
                {
                    var A = new StringBuilder();
                    int br = 1;
                    i++;
                    while(true)
                    {
                        if(s[i] == '(')
                        {
                            br++;
                        }
                        if(s[i] == ')')
                        {
                            br--;
                            if(br == 0)
                            {
                                break;
                            }
                        }
                        A.Append(s[i]);
                        i++;
                    }
                    S.Push(Polify(A.ToString()));
                    C.Push(zn);
                }
                else if(s[i] == '^')
                {
                    var A = new StringBuilder();
                    i++;
                    while(i<s.Length && Conv(s[i])>-1 && Conv(s[i])<10)
                    {
                        A.Append(s[i]);
                        i++;
                    }
                    i--;
                    int n = int.Parse(A.ToString());
                    var P1 = S.Pop();
                    var P2 = new Pol(P1);
                    for(int j = 1; j<n; j++)
                    {
                        P2 = Mult(P2,P1);
                    }
                    S.Push(P2);
                }
                else if(s[i] == '\'')
                {
                    var A = new StringBuilder();
                    int br = 1;
                    i++;
                    while(i<s.Length && s[i] == '\'')
                    {
                        br++;
                        i++;
                    }
                    i--;
                    var P1 = S.Pop();
                    for(int j = 0; j<br; j++)
                    {
                        P1 = Pol.Przv(P1);
                    }
                    S.Push(P1);
                }
            }
            while(S.Count != 0)
            {
                if(C.Peek() == '+')
                {
                    P = Add(P,S.Pop());
                    C.Pop();
                }
                else if(C.Peek() == '-')
                {
                    P = Subtract(P,S.Pop());
                    C.Pop();
                }
                else if(C.Peek() == '*')
                {
                    var P1 = S.Pop();
                    var P2 = S.Pop();
                    S.Push(Mult(P1,P2));
                    C.Pop();
                }
                else if(C.Peek() == '/')
                {
                    var P1 = S.Pop();
                    var P2 = S.Pop();
                    C.Pop();
                    if(P1.Z == null)
                    {
                        if(P2.Z == null)
                        {
                            P2.Z = P1;
                        }
                        else
                        {
                            P2.Z = Mult(P2.Z,P1);
                        }
                        S.Push(P2);
                    }
                    else
                    {
                        var P3 = new Pol(P1);
                        var P4 = new Pol(P1.Z);
                        P3.Z = null;
                        P4.Z = P3;
                        P4 = Mult(P4,P2);
                        S.Push(P4);
                    }
                }
            }
            return P;
        }
        public static string Output(Pol P)
        {
            P.D = P.D.OrderByDescending(w => w.Key).ToDictionary(w => w.Key, w => w.Value);
            var A = new StringBuilder();
            if(P.Z != null)
            {
            	A.Append('(');
            }
            if(P.D.Count != 0)
            {
                float stf = 0;
                float kf = 0;
                if(P.D.Count>1)
                {
                    stf = P.D.Keys.ElementAt(0);
                    kf = P.D.Values.ElementAt(0);
                    if(stf>1)
                    {
                        if(kf != 1 && kf != -1)
                        {
                            A.Append(kf + "x^" + stf + " ");
                        }
                        else
                        {
                            if(kf == -1)
                            {
                                A.Append('-');
                            }
                            A.Append("x^" + stf + " ");
                        }
                    }
                    else if(stf == 1)
                    {
                        if(kf != 1 && kf != -1)
                        {
                            A.Append(kf + "x ");
                        }
                        else
                        {
                            if(kf == -1)
                            {
                                A.Append('-');
                            }
                            A.Append("x ");
                        }
                    }
                }
                //
                for(int i = 1; i<P.D.Count-1; i++)
                {
                    float st = P.D.Keys.ElementAt(i);
                    float k = P.D.Values.ElementAt(i);
                    if(k>0)
                    {
                        if(k != 1)
                        {
                            A.Append("+ " + k + "x^" + st + " ");
                        }
                        else
                        {
                            A.Append("+ " + "x^" + st + " ");
                        }
                    }
                    else if(k<0)
                    {
                        if(k != -1)
                        {
                            A.Append("- " + -k + "x^" + st + " ");
                        }
                        else
                        {
                            A.Append("- " + "x^" + st + " ");
                        }
                    }
                }
                stf = P.D.Keys.ElementAt(P.D.Count-1);
                kf = P.D.Values.ElementAt(P.D.Count-1);
                if(stf == 0)
                {
                    if(kf>0)
                    {
                        A.Append("+ " + kf);
                    }
                    else if(kf<0)
                    {
                        A.Append("- " +  -kf);
                    }
                }
                else
                {
                    if(kf>0)
                    {
                        if(kf != 1)
                        {
                            A.Append("+ " + kf + "x^" + stf + " ");
                        }
                        else
                        {
                            A.Append("+ " + "x^" + stf + " ");
                        }
                    }
                    else if(kf<0)
                    {
                        if(kf != -1)
                        {
                            A.Append("- " + -kf + "x^" + stf + " ");
                        }
                        else
                        {
                            A.Append("- " + "x^" + stf + " ");
                        }
                    }
                }
            }
            if(P.Z != null)
            {
                A.Append(")/("+Output(P.Z)+")");
            }
            return A.ToString();
        }
        public static int Conv(char k)
        {
            return Convert.ToInt32(k)-48;
        }
        public static List<Pol> Div(Pol A, Pol B)
        {
            var A1 = new Pol(A);
            var R = new Pol();
            float max = A.D.Keys.Max();
            float max2 = B.D.Keys.Max();
            //float kf = 0;
            for(float i = max; i>=max2; i--)
            {
                if(A1.D.ContainsKey(i))
                {
                    var P = new Pol();
                    P.D.Add(i-max2,A1.D[i]/B.D[max2]);
                    R.D.Add(i-max2,A1.D[i]/B.D[max2]);
                    P = Mult(P,B);
                    A1 = Subtract(A1,P);
                }
            }
            var L = new List<Pol>();
            L.Add(R);
            L.Add(A1);
            return L;
        }
        public static double Horner(Pol A, double x)
        {
            double result = A.D.Values.ElementAt(0);
            if(A.D.Count == 1 && A.D.Keys.ElementAt(0) != 0)
            {
                result *= x;
            }
            for(int i = 1; i<A.D.Count; i++)
            {
                try
                {
                    result = result*x+A.D.Values.ElementAt(i);
                }
                catch(OverflowException)
                {
                    return 0;
                }
            }
            if(A.Z != null)
            {
                try
                {
                    result /= Horner(A.Z,x);
                }
                catch(OverflowException)
                {
                    return 0;
                }
            }
            return result;
        }
        public static Pol Przv(Pol A)
        {
            if(A.Z == null)
            {
                var P = new Pol();
                foreach(KeyValuePair<float,float> kvp in A.D)
                {
                    if(kvp.Key != 0)
                    {
                        P.D.Add(kvp.Key-1,kvp.Key*kvp.Value);
                    }
                }
                return P;
            }
            var Z1 = new Pol(A.Z);
            var P1 = new Pol(A);
            P1.Z = null;
            var P2 = Pol.Subtract(Pol.Mult(Przv(P1),Z1),Pol.Mult(Przv(Z1),P1));
            P2.Z = Pol.Mult(A.Z,A.Z);
            return P2;
        }
    }
}
