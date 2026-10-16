using System;

namespace backend.Core;


//NOT IMPLEMENTED
public static class AudioFileMetadataExtractor
{
    private const int Int32Size = 32;
    private const int RawTypeHeaderSize = 4;
    private const int FrameSyncPosition = 11; 
    private const int MPEGVersionPosition = 13;
    private const int LayerPosition = 15;
    private const int ProtectionBitPosition = 16;
    private const int BitrateIndexPosition = 20;
    private const int SamplingRatePosition = 22;
    private const int PaddingBitPosition = 23;
    private const int privateBitPosition = 26;
    
    private enum MPEGVersion : uint
    {
        MPEG1 = 0b11, MPEG2 = 0b10, MPEG2_5 = 0b00
    }

    public static void Mp3Metadata(IFormFile file, AudioFileType audioType)
    {
        
        Stream audioStream = file.OpenReadStream();

        if(audioType == AudioFileType.Raw)
        {
            byte[] buffer = new byte[RawTypeHeaderSize];
            audioStream.Read(buffer, 0, RawTypeHeaderSize);
            uint audioHeader = System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(buffer);

            uint syncword = audioHeader >> Int32Size - FrameSyncPosition;

            if (syncword != 0x7FF)
                return;

            MPEGVersion MPEGVersion = (MPEGVersion)((audioHeader >> Int32Size - MPEGVersionPosition) & 0b11);
            Console.WriteLine($"MPEGVersion: {MPEGVersion}");

            
            uint Layer = (audioHeader >> Int32Size - LayerPosition) & 0b11;

            if(Layer != 0b01)
            {   
                Console.WriteLine($"Not Layer 3: {Layer:b}");
                return;
            }
            uint CRC = (audioHeader >> Int32Size - ProtectionBitPosition) & 0b1;

            //Implement CRC support
            if(CRC != 0b1)
                return;

            
            // a >>= 4;
            // Console.WriteLine($"Bit po: {a:x2}"); 

            // Console.WriteLine($"Buffer przed: {BitConverter.ToString(buffer)}");
            // buffer[0] >>= 8; 
            // Console.WriteLine($"Buffer po: {BitConverter.ToString(buffer)}");
        }

        //audioStream.Position = ID3v2Version;


        //Console.WriteLine($"Major: {buffer[0]:x2}, Minor: {buffer[1]:x2}");

        //Console.WriteLine(BitConverter.ToString(buffer)); 

        audioStream.Close();
    }
}
