using System;

namespace Game
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (!DbManager.Connect("game", "127.0.0.1", 3306, "root", "123456"))
			{
				return;
			}
			NetManager.StartLoop(8888);



			//MsgMove msgMove = new MsgMove();
			//msgMove.x = 100;
			//msgMove.y = -20;
			////相当于取得要发送的字符串
			//byte[] bytes = MsgBase.Encode(msgMove);
			////当作接收后解码
			//string s = System.Text.Encoding.UTF8.GetString(bytes);
			//Console.WriteLine(s);
			//Console.WriteLine("Hello World!");

			//s = "{\"protoName\":\"MsgMove\",\"x\":100,\"y\":-20,\"z\":10}";
			//byte[] bytes1 = System.Text.Encoding.UTF8.GetBytes(s);
			////解码
			//MsgMove m = (MsgMove)MsgBase.Decode("MsgMove",bytes1,0,bytes1.Length);

			//Console.WriteLine(m.x);
   //         Console.WriteLine(m.y);
   //         Console.WriteLine(m.z);
   //         Console.ReadLine();

        }
	}
}
