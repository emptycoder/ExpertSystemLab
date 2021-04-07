using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExpertSystemUI.Entities.Readers
{
    public class CommandReader : IDisposable
    {
        private static readonly HashSet<char> skipCharacters = new() {'﻿', '\n', '\r', '\t'};
        private static readonly Decoder decoder = Encoding.UTF8.GetDecoder();
        private readonly Stream _stream;
        private readonly byte[] _byteBuffer = new byte[1024];
        private int _bytePos;
        private int _byteLen;
        private readonly char[] _charBuffer = new char[Encoding.UTF8.GetMaxCharCount(1024)];
        private int _charLen;
        private int _charPos;

        public CommandReader(string filePath) =>
            _stream = new FileStream(filePath, FileMode.Open);

        public CommandReader(Stream stream) =>
            _stream = stream;

        public bool EndOfStream
        {
            get
            {
                if (_charPos < _charLen)
                    return false;
                return ReadNextBuffer() == 0;
            }
        }

        public string ReadNextCommand()
        {
            if (_charPos == _charLen && ReadNextBuffer() == 0) return null;
            bool commentArea = false;
            bool saveFormatArea = false;
            StringBuilder sb = null;
            do
            {
                do
                {
                    char character = _charBuffer[_charPos++];
                    if (commentArea && (character == '\n' || character == '\r'))
                        commentArea = false;
                    else if (commentArea)
                        continue;
                    else if (character == '#')
                        commentArea = true;
                    else if (character == '`')
                        saveFormatArea = !saveFormatArea;
                    else if (!saveFormatArea && skipCharacters.Contains(character))
                        continue;
                    else if (character == ';')
                    {
                        return sb?.ToString();
                    }
                    else
                    {
                        sb ??= new StringBuilder(80);
                        sb.Append(character);
                    }
                } while (_charPos < _charLen);
            } while (ReadNextBuffer() > 0);

            return sb?.ToString();
        }

        private int ReadNextBuffer()
        {
            _charLen = 0;
            _charPos = 0;
            _byteLen = _stream.Read(_byteBuffer, _bytePos, _byteBuffer.Length - _bytePos);
            if (_byteLen > 0)
            {
                _charLen += decoder.GetChars(_byteBuffer, 0, _byteLen, _charBuffer, _charLen);
                _bytePos = _byteLen = 0;
            }

            return _charLen;
        }

        public void Dispose()
        {
            _stream.Close();
            _stream.Dispose();
        }
    }
}