# Readme - Code Samples for Chapter 17, Parallel Programming

**Parallel Programming** covers myriad features available with .NET for parallelization and synchronization. Chapter 11 shows the core functionality of the `Task` class. In Chapter 17, more of the `Task` class is shown, such as forming task hierarchies and using value tasks. The chapter goes into issues of parallel programming such as race conditions and deadlocks, and for synchronization, you learn about different features available with the `lock` keyword, the `Monitor`, `SpinLock`, `Mutex`, `Semaphore` classes, and more.

This chapter contains the following code samples:

* Parallel Samples
    * ParallelSamples (using the `Parallel` class)
    * TaskSamples (using the `Task` class, task hierarchies)
    * ValueTaskSample (`ValueTask`)
    * CancellationSamples (`CancellationToken`)
    * SimpleDataFlowSample (DataFlow)
    * DataFlowSample (a pipeline using multiple blocks)
    * TimersSample (`Timer`)
    * WindowsAppTimer (Windows App - `DispatcherTimer`)
* Synchronization Samples
    * ThradingIssues (race condition and deadlocks)
    * SynchronizationSamples (`lock`, `Interlocked`, `Monitor`)
    * SingletonUsingMutex (`Mutex`)
    * SemaphoreSample (`SemaphoreSlim`)
    * EventSample (`ManualResetEventSlim`)
    * EventSampleWithCountdownEvent (`CountdownEvent`)
    * BarrierSample (`Barrier`)
    * ReaderWriterLockSample (`ReaderWriterLockSlim`)
    * LockAcrossAwait (using locks with multiple threads - with `SemaphoreSlim`)

The WindowsAppTimer sample needs to have WinUI installed. See [WinUI](../../WinUI.md) for information on installing and using the WinUI samples.
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!