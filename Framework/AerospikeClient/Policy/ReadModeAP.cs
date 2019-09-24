/* 
 * Copyright 2012-2019 Aerospike, Inc.
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
	/// <summary>
	/// Read policy for AP (availability) namespaces.
	/// <para>
	/// How duplicates should be consulted in a read operation.
	/// Only makes a difference during migrations and only applicable in AP mode.
	/// </para>
	/// </summary>
	public enum ReadModeAP
	{
		/// <summary>
		/// Involve single node in the read operation.
		/// </summary>
		ONE,

		/// <summary>
		/// Involve all duplicates in the read operation.
		/// </summary>
		ALL
	}
}