using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CalcGustavo1.Model
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public enum Operacao
        {
            Soma,
            Subtracao,
            Multiplicacao,
            Divisao
        }

        private decimal? _visor;

        public decimal? Visor
        {
            get { return _visor; }
            set
            {
                _visor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visor)));
            }
        }
        private Operacao? _operacao;
        private decimal _numero1;
        private decimal _numero2;

        public ICommand LimpaTudoCommand { get; }

        private void LimpaTudo()
        {
            _numero1 = 0;
            _numero2 = 0;
            _operacao = null;
            Visor = null;
        }
        public ViewModel()
        {
            _numero1 = 0;
            _numero2 = 0;

            LimpaTudoCommand = new Command(LimpaTudo);
            InsereNumeroCommand = new Command<string>(InsereNumero);
            InsereOperacaoCommand = new Command<string>(InsereOperacao);
            RealizaOperacaoCommand = new Command(RealizaOperacao);
        }
        public ICommand InsereNumeroCommand { get; }

        private void InsereNumero(string numeroInserido)
        {
            if (_operacao == null)
            {
                _numero1 = _numero1 * 10 + Convert.ToDecimal(numeroInserido);
                Visor = _numero1;
            }
            else
            {
                _numero2 = _numero2 * 10 + Convert.ToDecimal(numeroInserido);
                Visor = _numero2;
            }


        }
        public ICommand InsereOperacaoCommand { get; }
        public void InsereOperacao(string operacao)
        {
            if (operacao == "+")
                _operacao = Operacao.Soma;
            if (operacao == "-")
                _operacao = Operacao.Subtracao;
            if (operacao == "*")
                _operacao = Operacao.Multiplicacao;
            if (operacao == "/")
                _operacao = Operacao.Divisao;
        }
        public ICommand RealizaOperacaoCommand { get; }
        private void RealizaOperacao()
        {
            switch (_operacao)
            {
                case Operacao.Soma:
                    _numero1 = _numero1 + _numero2;
                    break;
                case Operacao.Subtracao:
                    _numero1 = _numero1 - _numero2;
                    break;
                case Operacao.Multiplicacao:
                    _numero1 = _numero1 * _numero2;
                    break;
                case Operacao.Divisao:
                    try
                    {
                        _numero1 = _numero1 / _numero2;
                    }
                    catch
                    {
                        _numero1 = 0;
                    }
                    break;
                case null:
                    return;
            }

            Visor = _numero1;
            _numero2 = 0;
            _operacao = null;
        }
    }


}
