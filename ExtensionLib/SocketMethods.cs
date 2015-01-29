//-----------------------------------------------------------------------
// <copyright file="SocketMethods.cs" company="LouisTakePILLz">
// Copyright © 2015 LouisTakePILLz
// <author>LouisTakePILLz</author>
// </copyright>
//-----------------------------------------------------------------------

/*
 * This program is free software: you can redistribute it and/or modify it under the terms of
 * the GNU General Public License as published by the Free Software Foundation, either
 * version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Linq;
using System.Net.Sockets;

namespace ExtensionLib
{
    /// <summary>
    /// Provides various socket-related static extension methods.
    /// </summary>
    public static class SocketMethods
    {
        /// <summary>
        /// Sets the <see cref="System.Net.Sockets.IOControlCode.KeepAliveValues"/> options of a socket.
        /// </summary>
        /// <param name="socket">The socket to modify.</param>
        /// <param name="enabled">The state of the keep-alive operating mode.</param>
        /// <param name="timeout">The duration to wait, in milliseconds, before the connection times out.</param>
        /// <param name="retryInterval">The interval to wait, in milliseconds, between keep-alive failures.</param>
        public static void SetKeepAlive(this Socket socket, Boolean enabled, UInt32 timeout, UInt32 retryInterval)
        {
            socket.IOControl(IOControlCode.KeepAliveValues, new[]
            {
                (UInt32) (enabled ? 1 : 0),
                timeout,
                retryInterval
            }.SelectMany(BitConverter.GetBytes).ToArray(), null);
        }

        /// <summary>
        /// Determines whether a socket is available for reading.
        /// </summary>
        /// <param name="socket">The socket to poll.</param>
        /// <param name="pollingTime">The time to wait for a response, in microseconds.</param>
        /// <returns>A boolean value indicating whether the socket has replied.</returns>
        public static Boolean IsAlive(this Socket socket, Int32 pollingTime = 1000)
        {
            return socket.Connected && !(socket.Poll(pollingTime, SelectMode.SelectRead) && socket.Available == 0);
        }
    }
}
