/* 
 * Copyright 2012-2018 Aerospike, Inc.
 *
 * Portions may be licensed to Aerospike, Inc. under one or more contributor
 * license agreements.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */
namespace Aerospike.Client
{
	public sealed class BufferPool
	{
		public const int BUFFER_CUTOFF = 1024 * 128; // 128 KB

		public readonly byte[] buffer;
		public readonly int bufferSize;

		/// <summary>
		/// Construct one large contiguous cached buffer for use in asynchronous socket commands.
		/// Each command will use a segment of this large buffer.
		/// </summary>
		public BufferPool(int maxCommands, int size)
		{
			// Round up buffer size in 8K increments.
			int rem = size % 8192;

			if (rem > 0)
			{
				size += 8192 - rem;
			}
			bufferSize = size;

			// Allocate one large buffer which will likely be placed on LOH (large object heap).
			// This heap is not usually compacted, so pinning and fragmentation becomes less of 
			// an issue.
			buffer = new byte[maxCommands * bufferSize];
		}

		public BufferPool()
		{
		}

		public void GetNextBuffer(BufferSegment segment)
		{
			segment.buffer = buffer;
			segment.offset = bufferSize * segment.index;
			segment.size = bufferSize;

			if (segment.offset >= buffer.Length)
			{
				throw new AerospikeException("BufferPool overflow: " + bufferSize + ',' +
					segment.offset + ',' + buffer.Length);
			}
		}
	}

	public sealed class BufferSegment
	{
		public byte[] buffer;
		public readonly int index;
		public int offset;
		public int size;

		public BufferSegment(int index)
		{
			this.index = index;
		}

		public BufferSegment(int index, int size)
		{
			this.buffer = new byte[size];
			this.index = index;
			this.offset = 0;
			this.size = size;
		}

		public override string ToString()
		{
			string str = (buffer != null) ? buffer.Length.ToString() : "null";
			return "[" + str + ',' + index + ',' + offset + ',' + size + ']';
		}
	}
}
