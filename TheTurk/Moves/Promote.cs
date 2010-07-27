﻿using System.Collections.Generic;
using ChessEngine.Main;
using ChessEngine.Pieces;

namespace ChessEngine.Moves
{

    public class Promote : Ordinary
    {
        #region PromotionType enum

        public enum PromotionType
        {
            Queen, Rook, Bishop, Knight
        }

        #endregion

        readonly Piece PromotedPiece;

        public Promote(Board board, Piece piece, Coordinate to, PromotionType type)
            : base(board, piece, to)
        {
            

            var color = piece.Color;
            switch (type)
            {
                case PromotionType.Queen: PromotedPiece = new Queen(to, piece.Color);
                    break;
                case PromotionType.Rook: PromotedPiece = new Rook(to, piece.Color);
                    break;
                case PromotionType.Bishop: PromotedPiece = new Bishop(to, piece.Color);
                    break;
                case PromotionType.Knight: PromotedPiece = new Knight(to, piece.Color);
                    break;
                default:
                    break;
            }


        }

        public static List<Move> AllPossiblePromotions(Board board, Piece piece, Coordinate square)
        {
            var moves = new List<Move>();
            Move move = new Promote(board, piece, square, Promote.PromotionType.Queen);
            moves.Add(move);
            move = new Promote(board, piece, square, Promote.PromotionType.Rook);
            moves.Add(move);
            move = new Promote(board, piece, square, Promote.PromotionType.Bishop);
            moves.Add(move);
            move = new Promote(board, piece, square, Promote.PromotionType.Knight);
            moves.Add(move);
            return moves;
        }
        public override void MakeMove(Board board)
        {
            piece.RemoveMe(board);
            CapturedPiece = To.GetPiece(board);
            board[To] = PromotedPiece;
        }
        public override void UnMakeMove(Board board)
        {
            PromotedPiece.RemoveMe(board);
            if (CapturedPiece != null)
            {
                CapturedPiece.PutMe(board);
            }
            piece.PutMe(board);
        }
        public override string Notation()
        {
            string notation = "=" + PromotedPiece.NotationLetter;
            return base.Notation() +notation;
        }
    }
}
