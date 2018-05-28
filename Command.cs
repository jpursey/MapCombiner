using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCombiner
{
    interface ICommand
    {
        void Redo();
        void Undo();
    }

    class Commands
    {
        public Commands()
        {
            m_index = 0;
            m_commands = new List<ICommand>();
        }

        public bool CanUndo { get { return m_index > 0; } }
        public bool CanRedo { get { return m_index < m_commands.Count; } }

        public void Do(ICommand command)
        {
            if (m_index < m_commands.Count)
                m_commands.RemoveRange(m_index, m_commands.Count - m_index);
            command.Redo();
            m_commands.Add(command);
            m_index = m_commands.Count;
        }

        public void Undo()
        {
            if (CanUndo)
            {
                m_index -= 1;
                m_commands[m_index].Undo();
            }
        }

        public void Redo()
        {
            if (CanRedo)
            {
                m_commands[m_index].Redo();
                m_index += 1;
            }
        }

        public void Reset()
        {
            m_commands.Clear();
            m_index = 0;
        }

        private int m_index;
        private List<ICommand> m_commands;
    }
}
