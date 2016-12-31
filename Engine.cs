using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess_Test_1
{
    class Engine
    {
        Evaluation Evaluator;
        private int Depth;
        
        public Engine(Evaluation Evo)
        {
            Evaluator = Evo;
        }

        public void setDepth(int _Depth)
        {
            Depth = _Depth;
        }
        public Move generateNextMove()
        {
            Move BestMove = null;
            int EvaluateValue;
            int BestValue= Int32.MinValue;
            int alpha = Int32.MinValue;
            int beta = Int32.MaxValue;
            Evaluator.generateLegalMoves();
            List<Move> LegalMoves = Evaluator.LegalMoves;
            foreach (Move LegalMove in LegalMoves)
            {
                Evaluator.makeMove(LegalMove);
                    EvaluateValue = AlphaBetaMin(alpha,beta,Depth-1);
                    Evaluator.reverseMove(LegalMove);
                    if (EvaluateValue >= BestValue)
                    {
                        BestValue = EvaluateValue;
                        BestMove = LegalMove;
                    }
            }

            return BestMove;
        }
        private int AlphaBetaMax(int alpha, int beta, int depthleft)
        {
            bool WhiteToPlay = Evaluator.isWhiteToPlay;
            if (depthleft == 0)
                return Evaluator.evaluate();
            Evaluator.generateLegalMoves();
            List<Move> LegalMoves = Evaluator.LegalMoves;
         
            if (LegalMoves.Count == 0)
            {
                if (Evaluator.isCheckMata())
                    if (WhiteToPlay)
                        return Int32.MaxValue;
                    else
                        return Int32.MinValue;
                if (Evaluator.isStaleMate())
                    return 0;
            }
            foreach (Move legalmove in LegalMoves)
            {
                Evaluator.makeMove(legalmove);
                
                int movesscore = AlphaBetaMin(alpha, beta, depthleft - 1);
                Evaluator.reverseMove(legalmove);

                if(movesscore > alpha)
                    alpha = movesscore;
                if (alpha >= beta)
                    return alpha;
                
            }
            return alpha;
        }

        private int AlphaBetaMin(int alpha, int beta, int depthleft)
        {
            bool WhiteToPlay = Evaluator.isWhiteToPlay;
            if (depthleft == 0)
                return Evaluator.evaluate();

            Evaluator.generateLegalMoves();
            List<Move> LegalMoves = Evaluator.LegalMoves;
            if (LegalMoves.Count == 0)
            {

                if (Evaluator.isCheckMata())
                    if (WhiteToPlay)
                        return Int32.MaxValue;
                    else
                        return Int32.MinValue;
                if (Evaluator.isStaleMate())
                    return 0;
            }
         
            foreach (Move legalmove in LegalMoves)
            {

                Evaluator.makeMove(legalmove);
                int movescore = AlphaBetaMax(alpha, beta, depthleft - 1);
                Evaluator.reverseMove(legalmove);

                 if (movescore < beta)
                    beta = movescore;
                if (beta <= alpha)
                    return beta;
               
            }
            return beta;
        }

    }
}
