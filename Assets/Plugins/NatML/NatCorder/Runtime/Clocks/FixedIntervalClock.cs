/* 
*   NatCorder
*   Copyright (c) 2022 NatML Inc. All Rights Reserved.
*/

namespace NatSuite.Recorders.Clocks {

    using System.Runtime.CompilerServices;

    /// <summary>
    /// Clock that produces timestamps spaced at a fixed interval.
    /// This clock is useful for enforcing a fixed framerate in a recording.
    /// </summary>
    public sealed class FixedIntervalClock : IClock {

        #region --Client API--
        /// <summary>
        /// Interval between consecutive timestamps generated by the clock in seconds.
        /// </summary>
        public readonly double interval;

        /// <summary>
        /// Current timestamp in nanoseconds.
        /// The very first value reported by this property will always be zero.
        /// </summary>
        public long timestamp {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get => (long)((autoTick ? ticks++ : ticks) * interval * 1e+9);
        }

        /// <summary>
        /// Create a fixed interval clock for a given framerate.
        /// </summary>
        /// <param name="framerate">Desired framerate for clock's timestamps.</param>
        /// <param name="autoTick">Optional. If true, the clock will tick when its `Timestamp` is accessed.</param>
        public FixedIntervalClock (float framerate, bool autoTick = true) {
            this.interval = 1.0 / framerate;
            this.ticks = 0L;
            this.autoTick = autoTick;
        }

        /// <summary>
        /// Advance the clock by its time interval.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Tick () => ticks++;
        #endregion


        #region --Operations--
        private readonly bool autoTick;
        private long ticks;
        #endregion
    }
}