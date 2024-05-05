#FixMessages parser faster

Projeto visa demonstrar ganhos de performance com uso de _Slice_ e _Span<char>_ que foi introduzido no _C# 10_ para realização de parser de uma mensagem _FIX_.
Após comparativos com uso de _Split_ observou o seguinte ganho medio.

## Resultados de Benchmark

// _ Detailed results _
FixParseBenchmark.ParseMessage: DefaultJob
Runtime = .NET 8.0.4 (8.0.424.16909), Arm64 RyuJIT AdvSIMD; GC = Concurrent Workstation
Mean = 1.827 us, StdErr = 0.001 us (0.07%), N = 15, StdDev = 0.005 us
Min = 1.817 us, Q1 = 1.823 us, Median = 1.827 us, Q3 = 1.830 us, Max = 1.836 us
IQR = 0.007 us, LowerFence = 1.812 us, UpperFence = 1.841 us
ConfidenceInterval = [1.821 us; 1.832 us] (CI 99.9%), Margin = 0.005 us (0.29% of Mean)
Skewness = -0.02, Kurtosis = 2.14, MValue = 2
-------------------- Histogram --------------------
[1.815 us ; 1.839 us) | @@@@@@@@@@@@@@@

---

FixParseBenchmark.ParseMessageFaster: DefaultJob
Runtime = .NET 8.0.4 (8.0.424.16909), Arm64 RyuJIT AdvSIMD; GC = Concurrent Workstation
Mean = 1.114 us, StdErr = 0.001 us (0.07%), N = 12, StdDev = 0.003 us
Min = 1.109 us, Q1 = 1.113 us, Median = 1.115 us, Q3 = 1.116 us, Max = 1.117 us
IQR = 0.003 us, LowerFence = 1.108 us, UpperFence = 1.121 us
ConfidenceInterval = [1.111 us; 1.118 us] (CI 99.9%), Margin = 0.003 us (0.31% of Mean)
Skewness = -0.64, Kurtosis = 2.05, MValue = 2
-------------------- Histogram --------------------
[1.107 us ; 1.119 us) | @@@@@@@@@@@@

---

Aqui temos 1.815 us - 1.107 us , uma diferença de 708 us (microsegundos) por operação em média sendo _0,000708 segundos_.
Somando cerca de 500.000 operações, temos um ganho de aproximadamente 6 segundos.
