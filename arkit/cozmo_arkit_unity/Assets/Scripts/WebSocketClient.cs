// Copyright (c) 2017 Anki, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License in the file LICENSE.txt or at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
namespace Cozmo
{
	namespace AR
	{
		using SocketIO;
		using UnityEngine;

		// Requires and implements Socket.IO.
		public class WebSocketClient : MonoBehaviour
		{
			private SocketIOComponent mSocket = null;

			private void Awake()
			{
				mSocket = GetComponent<SocketIOComponent>();

				if ( mSocket != null ) {
					mSocket.On( "open", OnOpen);
					mSocket.On( "close", OnClose);
					mSocket.On( "error", OnError);
					mSocket.On( "boop", OnBoop);
				}
			}

			public void OnBoop( SocketIOEvent e )
			{
				Debug.Log( "[SocketIO] Boop received: " + e.name + " " + e.data );

				// The server sends numbers representing values in 
				// the `FireworkController.LaunchType` enumerator
				if ( e.data != null && e.data.HasField( "message" ) ) {
					int t = 0;
					JSONObject msg = e.data.GetField( "message" );
					if ( msg != null && int.TryParse( msg.str, out t ) ) {
						FireworkController.Launch( (FireworkController.LaunchType)t );
					}
				}
			}

			public void OnClose( SocketIOEvent e )
			{	
				Debug.Log( "[SocketIO] Close received: " + e.name + " " + e.data );
			}

			public void OnError( SocketIOEvent e )
			{
				Debug.Log( "[SocketIO] Error received: " + e.name + " " + e.data );
			}

			public void OnOpen( SocketIOEvent e )
			{
				Debug.Log( "[SocketIO] Open received: " + e.name + " " + e.data );
			}
		}
	}
}
